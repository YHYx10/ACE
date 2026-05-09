<template>
  <div class="weapon-list">
    <WeaponComponent
      v-for="(weapon, index) in weapons"
      :key="index"
      :index="index"
      :title="loc(weapon.getName())"
      :name="weaponType"
      :price="weapon.price"
      :ammoType="getAmmoType(index)"
      :weight="weapon.getWeight()"
      :isCurrent="current === index"
      :img="weapon.config.Image"
      @onSelect="$emit('onSelect', index)"
    />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import WeaponComponent from './WeaponComponent.vue'

export default {
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  props: {
    weaponType: String,
    weapons: Array,
    current: Number,
  },
  components: { WeaponComponent },
  methods: {
    getAmmoType(index) {
      return (this.loc(this.weapons[index].getAmmoType()) + '')
        .split(' ')
        .slice(1, 2)
        .join('')
    },
  },
}
</script>

<style lang="scss" scoped>
.weapon-list {
  width: 33.86;
  flex-shrink: 0;
  height: 80.09vh;
  overflow-y: scroll;
  overflow-x: hidden;
  padding-right: 1.67vh;
  .weapon-component {
    margin: 1.57vh 0;
  }
  &::-webkit-scrollbar {
    width: 0.28vh;
  }
  &::-webkit-scrollbar-track {
    background: rgba(255, 255, 255, 0.23);
  }
  &::-webkit-scrollbar-thumb {
    background: #ff0000;
  }
}
</style>
