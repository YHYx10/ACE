<template>
  <div class="modal-rank">
    <Back @click="closeModal" class="back" />
    <div class="content">
      <div class="title">
 Editing window
      </div>
      <div class="columns">
        <div class="col-left">
          <Title :margin="false" text="Family name" />
          <Input
            :placeholder="loc('fam:menu:plh:name')"
            v-model="currentRank.rankName"
          />
          <div
            v-if="currentRank.rankId !== 0"
            class="house-wrap"
          >
            <div class="row">
              <div class="name">Zugriff auf das Haus</div>
              <Selector
                :list="accessHouseList.list"
                :currentIndex="currentRank.accessHouse"
                @onSelect="setAccessHouse"
              />
            </div>
            <div class="row">
              <div class="name">Zugang zu Möbel</div>
              <Selector
                :list="accessFurnitureList.list"
                :currentIndex="currentRank.accessFurniture"
                @onSelect="setAccessFurniture"
              />
            </div>
          </div>
          <div
            v-if="currentRank.rankId !== 0"
            class="switch-wrap"
          >
            <div class="row">
              <div class="name">Zugang zu Familienkleidung</div>
              <Switcher v-model="currentRank.accessClothes" />
            </div>
            <div class="row">
              <div class="name">Access to the wars of business</div>
              <Switcher v-model="currentRank.accessWar" />
            </div>
            <div class="row">
              <div class="name">Verwaltung der Mitglieder</div>
              <Switcher v-model="currentRank.accessMembers" />
            </div>
          </div>
        </div>
        <div
          v-if="currentRank.rankId !== 0"
          class="col-right"
        >
          <Title :margin="false" text="Zugang zu Autos" />
          <div class="vehicle-list">
            <div
              class="row"
              v-for="(item, index) in currentRank.accessCars"
              :key="index"
            >
              <div class="name">{{ item.carName }}</div>
              <Selector
                :list="accessCarsList.list"
                :currentIndex="currentRank.accessCars[index].access"
                @onSelect="(number) => setAccessCar(number, index)"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="btn-list">
        <DefaultBtn @click="setRank">save</DefaultBtn>
        <DefaultBtn @click="copyArray" secondary>cancel</DefaultBtn>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import DefaultBtn from '../../../UI/button/DefaultBtn.vue'
import Back from '../../components/Back.vue'
import Input from '../components/Input.vue'
import Selector from '../components/Selector.vue'
import Switcher from '../components/Switcher.vue'
import Title from '../components/Title.vue'
export default {
  props: {
    rank: Object,
  },
  components: {
    Back,
    Title,
    Input,
    Selector,
    Switcher,
    DefaultBtn,
  },
  computed: {
    ...mapState('familyMenu', ['isLeader']),
    ...mapState('familyMenu/controlPage', [
      'accessHouseList',
      'accessFurnitureList',
      'accessCarsList',
    ]),
    ...mapGetters('localization', ['loc']),
  },
  data: function() {
    return {
      currentRank: null,
    }
  },
  methods: {
    // ...mapMutations('familyMenu/controlPage', ['updateRank']),
    closeModal: function() {
      this.$emit('closeModal')
    },
    copyArray: function() {
      this.currentRank = { ...this.rank }
      this.currentRank.accessCars = []
      this.currentRank.accessStore = []
      this.rank.accessCars.forEach((element) => {
        this.currentRank.accessCars.push({ ...element })
      })
    },
    setAccessFurniture(value) {
      this.currentRank.accessFurniture = value
    },
    setAccessHouse(value) {
      this.currentRank.accessHouse = value
    },
    setAccessCar(value, index) {
      this.currentRank.accessCars[index].access = value
    },
    setRank: function() {
      window.mp.trigger('familyMenu:setRank', JSON.stringify(this.currentRank))
      this.closeModal()
      // setTimeout(() => this.updateRank(this.currentRank), 2000)
    },
  },
  beforeMount() {
    this.copyArray()
  },
}
</script>

<style lang="scss" scoped>
div,
span,
button,
input {
  font-family: 'Akrobat';
  font-style: normal;
  color: #ffffff;
}
.modal-rank {
  width: 100vw;
  height: 100vh;
  background: black;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 5;
  display: flex;
  align-items: center;
  justify-content: center;
  .back {
    position: absolute;
    left: 8.889vh;
    top: 4.722vh;
  }

  .content {
    display: flex;
    flex-direction: column;
    gap: 5.463vh;
    .title {
      font-weight: 800;
      font-size: 3.704vh;
      line-height: 4.444vh;
      display: flex;
      justify-content: center;
      text-transform: uppercase;
    }
    .columns {
      display: flex;
      gap: 5.463vh;

      div {
        .row {
          display: flex;
          justify-content: space-between;
          align-items: center;
        }
      }

      .col-left {
        width: 38.519vh;
        display: flex;
        flex-direction: column;
        .input-block {
          margin-top: 2.593vh;
          margin-bottom: 4.167vh;
        }
        .house-wrap {
          display: flex;
          flex-direction: column;
          gap: 0.926vh;
          margin-bottom: 3.426vh;
        }
        .switch-wrap {
          display: flex;
          flex-direction: column;
          gap: 1.852vh;
        }
      }
      .col-right {
        width: 39.907vh;
        display: flex;
        flex-direction: column;
        .vehicle-list {
          margin-top: 2.315vh;
          overflow-y: scroll;
          height: 34.167vh;
          &::-webkit-scrollbar {
            width: 0.463vh;
          }
          &::-webkit-scrollbar-track {
            background: rgba(255, 255, 255, 0.04);
          }
          &::-webkit-scrollbar-thumb {
            background: #301934 ;
          }
          .row {
            margin-top: 0.093vh;
            width: 38.519vh;
          }
        }
      }
    }

    .btn-list {
      display: flex;
      gap: 1.852vh;
      justify-content: center;
      margin-top: 8.241vh;

      button {
        font-weight: 700;
        font-size: 2.222vh;
        width: 27.315vh;
        height: 6.944vh;
      }
    }
  }
}
</style>
