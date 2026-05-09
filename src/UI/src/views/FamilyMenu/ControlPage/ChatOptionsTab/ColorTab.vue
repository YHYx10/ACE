<template>
  <div class="color-tab">
    <div 
      :class="[{active: item === currentColor}, 'color-tab__item']"
      v-for="item in colorList"
      :key="item.key"
      :style="{backgroundColor: item}"
      @click="setCurrentColor(item)"
    ></div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'ColorTab',

  props: {
    colorList: Array,
    currentColor: String
  },

  computed: {
    ...mapState('familyMenu/controlPage', ['chatOptions'])
  },

  methods: {
    setCurrentColor: function(value) {
      this.$emit('setCurrentColor', value)
    }
  }
}
</script>

<style lang="scss" scoped>
.color-tab{
  display: grid;
  width: 49.074vh;
  height: 29.167vh;
  grid-template-columns: repeat(7, 5.093vh);
  grid-gap: 0.926vh;
  grid-auto-rows: 5.093vh;
  &__item{
    border: 0.093vh solid transparent;
    border-radius: 100%;
    transition: .2s;
    height: 5.093vh;
    &:hover{
      box-shadow: 0 0 1.481vh rgba(255, 255, 255, 0.5);
    }
    &.active, &:hover{
      border-color: #fff;
      transition: .2s;
    }
  }
}
</style>
