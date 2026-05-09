<template>
  <div class="weapon-items-block" v-show="componentsList.length">
    <div class="title">
      {{ loc(`wshop_comp_cat_${slotName.toLowerCase()}`) }}
    </div>
    <div class="list">
      <WeaponItem
        v-for="(item, index) in componentsList"
        @click="onSelect(index)"
        :key="index"
        :isCurrent="current === index"
        :img="`/img/weaponShop/components/${slotName.toLowerCase()}.png`"
      />
    </div>
  </div>
</template>
<script>
import { mapGetters } from 'vuex'
import ComponentSlots from './ComponentSlots'
import WeaponItem from './WeaponItem.vue'
export default {
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  props: {
    img: String,
    title: String,
    componentsList: Array,
    current: Number,
    slotName: String,
  },
  components: { WeaponItem },
  data() {
    return { ComponentSlots }
  },
  methods: {
    onSelect(index) {
      console.log('onSelect', index)
      if (this.current === index) index = -1
      this.$emit(
        'onSelect',
        index,
        this.slotName
          .split('')
          .map((el, index) => (index === 0 ? el.toLowerCase() : el))
          .join(''),
        ComponentSlots[this.slotName],
        {
          slot: this.slotName,
          price: this.componentsList[index]
            ? this.componentsList[index].Price
            : 0,
        }
      )
    },
  },
}
</script>

<style lang="scss" scoped>
.weapon-items-block {
  min-width: 8.15vh;
  height: 6.02vh;
  .title {
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 500;
    font-size: 1.3vh;
    line-height: 1.57vh;
    color: #ffffff;
  }
  .list {
    margin-top: 0.74vh;
    display: flex;
    gap: 0.74vh;
  }
}
</style>
