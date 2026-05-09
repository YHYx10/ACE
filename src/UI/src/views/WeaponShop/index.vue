<template>
  <div class="weapon-shop">
    <div class="wrap">
      <div class="header">
        <div>
          <TitleComponent
            :titlePrimary="'Store'"
            :titleSecondary="'Weapons'"
          />
          <MoneyBlock showTitle :balance = "getCash()" />
        </div>

        <SelectWeaponType
          :list="weaponTypes.plural"
          :currentType="selectedWeaponType"
          @onSelect="onWeaponTypeSelect"
        />
        <div class="balance-exit">
          <ExitButton @click="exit" />
        </div>
      </div>
      <div class="main">
        <SelectWeapon
          :weapons="filteredWeapons"
          :current="current"
          :weaponType="weaponTypes.singular[selectedWeaponType]"
          @onSelect="onWeaponSelect"
        />
        <div class="right-part">
          <div class="top">
            <ShowSelectedWeapon
              :title="getCurrentWeaponName()"
              :subtitle="weaponTypes.singular[selectedWeaponType]"
            />
            <SelectSkin
              :skins="getComponents().Skin || []"
              :current="currentSkin"
              :img="
                filteredWeapons[current]
                  ? filteredWeapons[current].config.Image
                  : ''
              "
              @onSelect="selectSkin"
            />
          </div>
          <div class="middle">
            <div class="weapon-items-list">
              <div>
                <WeaponItemsBlock
                  :componentsList="getComponents().Scope || []"
                  :current="currentComponent.scope"
                  :title="loc(`wshop_comp_cat_${'Scope'.toLowerCase()}`)"
                  :slotName="'Scope'"
                  @onSelect="selectComponent"
                />
              </div>

              <div class="inline">
                <WeaponItemsBlock
                  :componentsList="getComponents().Muzzle || []"
                  :current="currentComponent.muzzle"
                  :title="loc(`wshop_comp_cat_${'Muzzle'.toLowerCase()}`)"
                  :slotName="'Muzzle'"
                  @onSelect="selectComponent"
                />
                <WeaponItemsBlock
                  :componentsList="getComponents().FlashLight || []"
                  :current="currentComponent.flashLight"
                  :title="loc(`wshop_comp_cat_${'FlashLight'.toLowerCase()}`)"
                  :slotName="'FlashLight'"
                  @onSelect="selectComponent"
                />
                <WeaponItemsBlock
                  :componentsList="getComponents().Clip || []"
                  :current="currentComponent.clip"
                  :title="loc(`wshop_comp_cat_${'Clip'.toLowerCase()}`)"
                  :slotName="'Clip'"
                  @onSelect="selectComponent"
                />
              </div>
            </div>
            <AmmoAmount
              v-if="selectedWeaponType === 'ammo' && current !== -1"
              class="margin-right"
              @onChange="onAmmoChange"
            />
            <button v-if="selectedWeaponType !== 'ammo' && current !== -1" @click="buyAmmo" class="button-second margin-right">
              Muntion kaufen
            </button>
            <PaymentBlock
              style="width: 44.26vh;"
              :price="totalPrice()"
              :layout="'short'"
              @onBuy="buyWeapon"
              @onChangePay="onChangePay"
            />
          </div>
          <WeaponCharacteristic
            v-if="current !== -1 && filteredWeapons[current].isWeapon()"
            :ammoType="getCharacteristic().ammoType"
            :weight="getCharacteristic().weight"
            :maxAmmo="getCharacteristic().magazine"
            :shootingCharacteristics="this.filteredWeapons[this.current].config.Characteristics"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'
import SelectWeaponType from './SelectWeaponType.vue'
import SelectWeapon from './SelectWeapon.vue'
import TitleComponent from '../UI/components/TitleComponent.vue'
import MoneyBlock from '../UI/components/MoneyBlock.vue'
import SelectSkin from './SelectSkin.vue'
import ShowSelectedWeapon from './ShowSelectedWeapon.vue'
import WeaponItemsBlock from './WeaponItemsBlock.vue'
import PaymentBlock from '../UI/components/PaymentBlock.vue'
import WeaponCharacteristic from './WeaponCharacteristic.vue'
import ExitButton from '../UI/components/ExitButton.vue'
import Content from './Content'
import AmmoAmount from './AmmoAmount.vue'

export default {
  computed: {
    ...mapState('weaponShop', ['weapons', 'loadingWeapon','playerMoney', 'playerBank']),
    ...mapGetters('localization', ['loc']),
    filteredWeapons() {
      return this.weapons
        .filter(
          (w) =>
            w.type ===
            Object.keys(this.weaponTypes.plural).findIndex(
              (v) => v === this.selectedWeaponType
            )
        )
        .sort((a) => (a.id === this.currentAmmo ? -1 : 0))
    },
  },

  data() {
    return {
      current: -1,
      ammo: 0,
      cashtype: 'cash',
      currentSkin: -1,
      currentAmmo: -1,
      prices: {},
      currentComponent: {
        scope: -1,
        muzzle: -1,
        flashLight: -1,
        clip: -1,
      },
      selectedWeaponType: 'steelArms',
      selectedWeaponTypeKey: 1,
      weaponTypes: Content.weaponTypes,
      ammoTypes: {
        rifle: 120,
        pistol: 118,
        shotgun: 122,
        smg: 119,
        sniper: 121,
        musket: 205,
      },
    }
  },
  methods: {
    ...mapMutations('weaponShop', ['loadWeapon']),
    getCurrentWeaponName() {
      if (!this.filteredWeapons[this.current]) return 'Unknown'
      return this.loc(this.filteredWeapons[this.current].getName())
    },
    getCash(){
      if(this.cashtype == 'cash') return this.playerMoney;
      else return this.playerBank;
    },
    onChangePay(type){
      this.cashtype = type
    },
    getCharacteristic() {
      if (!this.filteredWeapons[this.current]) return {}
      return {
        ammoType: this.loc(this.filteredWeapons[this.current].getAmmoType())
          .split(' ')
          .slice(1, 2)
          .join(''),
        weight: this.filteredWeapons[this.current].getWeight(),
        magazine: this.getTotalMaxAmmo(),
      }
    },
    getTotalMaxAmmo() {
      return this.currentComponent.clip === -1
        ? this.filteredWeapons[this.current].config.MaxAmmo
        : this.getComponents().Clip[this.currentComponent.clip].MaxAmmo
    },
    getSkins() {
      if (!this.filteredWeapons[this.current]) return []
      return (this.filteredWeapons[this.current].getComponents() || []).Skin
    },
    getComponents() {
      if (!this.filteredWeapons[this.current]) return []
      return this.filteredWeapons[this.current].getComponents() || []
    },
    selectSkin(index, slot, component) {
      this.currentSkin = index
      this.onComponentSelect(slot, index, component)
    },
    selectComponent(index, key, slot, component) {
      this.currentComponent[key] = index
      this.onComponentSelect(slot, index, component)
    },
    resetWeaponComponents() {
      this.currentComponent = {
        scope: -1,
        muzzle: -1,
        flashLight: -1,
        clip: -1,
      }
    },
    onComponentSelect(slot, compIndex, component) {
      if (this.loadingWeapon) return
      const item = this.filteredWeapons[this.current]
      const price = component.price.toFixed()
      this.$set(this.prices, component.slot, Number(price))
      const isWeapon = item.isWeapon()
      this.loadWeapon({
        id: item.id,
        slot,
        compIndex,
        model: isWeapon ? -1 : item.getModel(),
      })
    },
    buyAmmo() {
      this.currentAmmo = this.filteredWeapons[this.current].config.AmmoType
      
      // this.current = 
      this.selectedWeaponType = 'ammo'
      this.onWeaponSelect(0)
    },
    onAmmoChange(val) {
      this.ammo = val
      this.$set(
        this.prices,
        'weapon',
        this.filteredWeapons[this.current].isAmmo()
          ? this.filteredWeapons[this.current].price * this.ammo
          : this.filteredWeapons[this.current].price
      )
    },
    totalPrice() {
      let total = 0
      for (const key in this.prices) {
        total += this.prices[key]
      }
      return total
    },
    onWeaponTypeSelect(key) {
      this.current = -1
      this.selectedWeaponType = key
      this.loadWeapon({
        id: -1,
        slot: -1,
        compIndex: -1,
        model: -1,
      })
    },
    onWeaponSelect(index) {
      this.current = index
      this.prices = {}
      const item = this.filteredWeapons[this.current]
      this.$set(
        this.prices,
        'weapon',
        item.isAmmo() ? item.price * this.ammo : item.price
      )
      this.resetWeaponComponents()
      const isWeapon = item.isWeapon()
      this.loadWeapon({
        id: item.id,
        slot: -1,
        compIndex: -1,
        model: isWeapon ? -1 : item.getModel(),
      })
      this.currentSkin = -1
    },
    buyWeapon(paymentType) {
      if (this.current == -1 || this.loadingWeapon) return
      const count = this.filteredWeapons[this.current].isAmmo() ? this.ammo : 1
      window.mp.trigger('cef:wshop:buy', count, paymentType === 'bank')
    },
    exit() {
      window.mp.trigger('cef:wshop:close')
    },
  },
  mounted() {
    console.log(this.weapons);
    if (process.env.NODE_ENV == 'development')
      this.$store.commit('weaponShop/setWeapons', [
        [52, 1800],
        [55, 2880],
        [93, 5760],
        [69, 5000],
        [66, 4320],
        [67, 4320],
        [68, 4320],
        [120, 4320],
        [67, 4320],
        [68, 4320],
        [120, 4320],
        [67, 4320],
        [68, 4320],
        [118, 4320],
        [119, 4320],
        [120, 4320],
        [121, 4320],
        [122, 4320],
        [205, 4320],
      ])
  },
  components: {
    SelectWeaponType,
    SelectWeapon,
    TitleComponent,
    MoneyBlock,
    SelectSkin,
    ShowSelectedWeapon,
    WeaponItemsBlock,
    PaymentBlock,
    WeaponCharacteristic,
    ExitButton,
    AmmoAmount,
  },
}
</script>

<style lang="scss" scoped>
*,
::before,
::after {
  box-sizing: border-box;
}
.weapon-shop {
  padding: 0 1.979vw;
  .wrap {
    .header {
      gap: 1.564vw;
      display: flex;
      align-items: flex-end;
      margin-bottom: 2.78vh;
      .balance-exit {
        position: absolute;
        top: 3.426vh;
        right: 3.519vh;
        display: flex;
        flex-direction: column;
      }
    }

    .main {
      display: flex;
      .right-part {
        margin-left: 2.78vh;
        width: 100%;
        .top {
          display: flex;
          justify-content: space-between;
          gap: 10.83vh;
        }
        .middle {
          width: 100%;
          display: flex;
          align-items: flex-end;
          .weapon-items-list {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            height: 13.15vh;
            width: 31.67vh;
            margin-right: auto;
            margin-top: 2.69vh;
            margin-bottom: 2.78vh;

            .inline {
              margin-top: 1.11vh;
              gap: 1.2vh;
              display: flex;
            }
          }
          .margin-right {
            margin-right: 0.92vh;
          }
          .button-second {
            width: 21.67vh;
            height: 5.65vh;
            color: #fff;
            display: flex;
            justify-content: center;
            align-items: center;
            background: transparent;
            outline: none;
            border: 0.09vh solid transparent;
            font-family: 'Montserrat';
            font-style: normal;
            font-weight: 700;
            font-size: 1.48vh;
            line-height: 1.85vh;
            transition: 0.4s;
            background: rgba(255, 255, 255, 0.05);
            border: 0.09vh solid rgba(255, 255, 255, 0.16);
            &:hover {
              transition: 0.05s;
              background: rgba(255, 255, 255, 0.2);
              border: 0.09vh solid rgba(255, 255, 255, 0.66);
            }
          }
        }
      }
    }
  }

  width: 100vw;
  height: 100vh;
  &::after {
    content: '';
    position: absolute;
    z-index: -1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(0deg, rgba(3,4,6,0.88) 0%, rgba(255,255,255,0) 40%, rgba(255,255,255,0) 60%, rgba(3,4,6,1) 100%);
  }
  background: radial-gradient(circle, rgba(255,255,255,0) 0%, rgba(255,255,255,0) 25%, rgba(3,4,6,0.8) 52%, rgba(3,4,6,1) 100%);
}
</style>
