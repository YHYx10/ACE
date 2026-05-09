<template>
  <div class="icon-tab">
    <div 
      :class="[{active: item === currentIcon}, 'item']"
      v-for="item in 20"
      :key="item.id"
      @click="setCurrentIcon(item)"
    >
      <div 
        class="icon"
        :style="{backgroundImage: 'url(/img/familyMenu/chatOptions/icon-' + item + '.svg)'}"
      ></div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'IconTab',

  props: {
    currentIcon: Number
  },
  
  methods: {
    setCurrentIcon: function(value) {
      this.$emit('setCurrentIcon', value)
    }
  },

  computed: {
    ...mapState('familyMenu/controlPage', ['chatOptions'])
  },
}
</script>

<style lang="scss" scoped>
.icon-tab{
  display: grid;
  width: 49.074vh;
  height: 29.167vh;
  grid-template-columns: repeat(4, 1fr);
  grid-auto-rows: repeat(5, 1fr);
  grid-column-gap: 0.889vh;
  grid-row-gap: 0.926vh;
  .item{
    border: 0.093vh solid rgba(255, 255, 255, 0.09);
    border-radius: 5.556vh;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: .2s;
    width: 11.574vh;
    height: 5.093vh;
    &:hover, &.active{
      background: #301934 ;
      transition: .2s;
    }
    .icon{
      width: 1.778vh;
      height: 1.778vh;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
    }
  }
}
</style>
