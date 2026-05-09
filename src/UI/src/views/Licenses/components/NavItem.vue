<template>
  <div 
    :class="[{'active': item === currentNav}, 'nav-item']"
    @click="setCurrentNav(item)"
  >
    <div class="col"></div>
    <img
      class="icon"
      :src="'/img/licenses/'+ item +'.svg'"
    />
  </div>
</template>

<script>
export default {
  name: 'NavItem',

  props: {
    item: String,
    setCurrentNav: Function,
    currentNav: String,
  },

  computed: {
    currentNavIcon: function() {
      let icon = this.item
      if (this.item === this.currentNav) {
        return 'active-'+ icon
      } else {
        return icon
      }
    }
  }
}
</script>

<style lang="scss" scoped>
  .nav-item{
    display: flex;
    align-items: center;
    justify-content: center;
    width: 2.95rem;
    height: 2.95rem;
    background: rgba(255, 255, 255, 0.1);
    outline: 0.05rem solid rgba(255, 255, 255, 0.09);
    outline-offset: -0.05rem;
    &:hover {
      background: rgba(255, 255, 255, 0.25);
    }
    &.active {
      background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    }
    &.active{ 
      .col {
        position: absolute;
        width: 0.09rem;
        background: #fff;
        height: 0.75rem;
        left: 0.3rem;
      }
    }
    .icon {
      width: 1.7rem;
      height: 1.7rem;
      background-repeat: no-repeat;
      background-size: cover;
    }
  }
</style>
