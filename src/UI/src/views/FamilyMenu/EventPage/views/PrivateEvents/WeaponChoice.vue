<template>
  <div class="choice weapon-choice">
    <HeadItem
      :title="loc('fam:btl:choice:targ')"
      :text="loc('fam:btl:choice:biz')"
    />
    <div class="weapon-list">
      <WeaponItem
        v-for="item in weaponsList"
        :key="item.key"
        :img="`/img/familyMenu/eventsPage/weapons/${item.img}.png`"
        :name="loc(item.name)"
        :ammo="'30 cartridgeses'"
        :selected="item.id === currentWeapon"
        @onSelect="setCurrentWeapon(item.id)"
      />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import HeadItem from '../../components/HeadItem.vue'
import WeaponItem from '../../components/WeaponItem.vue'
export default {
  name: 'WeaponChoice',
  props: {
    setCurrentWeapon: Function,
    currentWeapon: Number,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('familyMenu/eventsPage', ['weaponsList']),
  },
  components: { HeadItem, WeaponItem },
}
</script>

<style lang="scss" scoped>
.weapon-choice {
  display: flex;
  flex-flow: column;
  margin-left: 7.87vh;
  .weapon-list {
    margin-top: 1.852vh;
    display: grid;
    width: 72.222vh;
    overflow-y: auto;
    grid-template-columns: repeat(2, 1fr);
    gap: 0.926vh;
    &::-webkit-scrollbar {
      background-color: transparent;
      width: 0.296vh;
      &-thumb {
        background: rgba(255, 255, 255, 0.1);
        border-radius: 0.148vh;
      }
    }
    
  }
}
</style>
