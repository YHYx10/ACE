<template>
  <div class="elevator">
    <ExitCross class="exit-cross" />
    <div class="list">
      <div @click="start(key)" v-for="(item, key) in floors" :key="item" class="item" :class="{current: currentFloorIndex === key}">
        {{ loc(item) }}
        <svg xmlns="http://www.w3.org/2000/svg" width="30" height="24" fill="none" viewBox="0 0 30 24">
          <path fill="#fff" d="m23.291 12 6.615-10.994a.655.655 0 0 0-.072-.774.691.691 0 0 0-.771-.183L.425 11.382A.668.668 0 0 0 0 12c0 .272.168.516.426.619l28.636 11.333a.696.696 0 0 0 .772-.184.655.655 0 0 0 .072-.773L23.291 12ZM21.4 14.764l.46 1.528L13.978 13l7.423 1.764Zm.418-3.43-9.844.333 9.844-3.36v3.027Z"/>
        </svg>
      </div>
    </div>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'
import ExitCross from '../UI/components/ExitCross.vue'
export default {
  data() {
    return {
      floor: 0
    }
  },
  computed:{
    ...mapState("elevator", ["floors", "currentFloorIndex"]),
    ...mapGetters("localization", ["loc"])
  },
  components: {
    ExitCross
  },
  methods: {
    down(){
      this.floor--;
      if(this.floor < 0)this.floor = this.floors.length - 1;
    },
    up(){
      this.floor++;
      if(this.floor >= this.floors.length) this.floor = 0;
    },
    // stop(){
    //     window.mp.trigger('lift', 'stop');
    // },
    start(floor){
      window.mp.trigger('lift', 'start', floor);
    },
  },
  beforeDestroy() {
    this.$store.commit("elevator/reset")
  },
}
</script>

<style lang="scss" scoped>
.elevator {
  padding: 80px 90px 60px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translateX(-50%) translateY(-50%);
  background: rgba(23, 23, 28, 0.9);
  display: flex;
  justify-content: center;
  align-items: center;
  .exit-cross {
    position: absolute;
    top: 20px;
    right: 20px;
  }
  .list {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 10px 20px;
  }
  .item {
    width: 300px;
    height: 88px;
    display: flex;
    align-items: center;
    padding-left: 30px;
    background: rgba(56, 59, 64, 0.1);
    outline: 1px solid rgba(255, 255, 255, 0.09);
    font-weight: 700;
    font-size: 24px;
    line-height: 29px;
    color: #fff;
    transition: 0.3s ease-in;
    text-transform: uppercase;
    box-shadow: inset 0px 0px -4.1497rem rgba(75, 0, 130, 0.86);
    &:hover {
      box-shadow: inset 0px 0px 6.1497rem rgba(75, 0, 130, 0.86);
      // background: linear-gradient(180deg, rgba(71, 44, 132, 0.65) 0%, rgba(75, 0, 130, 0.65) 100%);
    }
    position: relative;
    svg {
      display: none;
      position: absolute;
      right: -20px;
      transform: translateX(100%);
    }
    &.current {
      svg {
        display: block;
      }
    }
    
    &:nth-child(odd) {
      svg {
        left: -20px;
        transform: translateX(-100%) scale(-1, 1);
      }
    }
  }
}
</style>