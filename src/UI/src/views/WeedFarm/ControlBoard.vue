<template>
  <div class="grass-production">
    <ExitCross @click="exit" class="exit-cross" />
    <div class="content">
      <div class="title">
      harvest
      </div>
      <div class="stages">
        <StageItem
          v-for="(value, _, index) of stady"
          :key="index"
          :index="index"
          :count="value"
        />
        <button @click="toWork">{{loc(onDeliveryJob ? 'weedfarm:cboard:btn:2':'weedfarm:cboard:btn')}}</button>
      </div>
      <img class="grass-bg" src="/img/weedFarm/trava.png" alt="" />
      <div class="title">
        equipment
      </div>
      <div class="systems">
        <SystemItem
          v-for="(value, _, index) in systems"
          :key="index"
          :index="index"
          :value="value"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import ExitCross from '../UI/components/ExitCross'
import StageItem from './StageItem.vue'
import SystemItem from './SystemItem.vue'

export default {
  computed: {
    ...mapState('weedFarm', [
      'profit',
      'ownerName',
      'systems',
      'stady',
      'onDeliveryJob',
    ]),
    ...mapGetters('localization', ['loc']),
  },
  data() {
    return {
      spam: 0,
    }
  },
  components: {
    ExitCross,
    StageItem,
    SystemItem,
  },
  methods: {
    toWork() {
      if (Date.now() < this.spam) return
      this.spam = Date.now() + 1000
      if (this.onDeliveryJob) window.mp.triggerServer('weedfarm:delivery:end')
      else window.mp.triggerServer('weedfarm:delivery:begine')
    },
    exit() {
        // mp.trigger ....
    }
  },
}
</script>

<style lang="scss" scoped>
.grass-production {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(28, 29, 34, 0.9);
  .exit-cross {
    position: absolute;
    top: 2rem;
    right: 2rem;
  }
  .content {
    display: flex;
    flex-direction: column;
    .title {
      font-weight: 700;
      font-size: 2rem;
      line-height: 2.4rem;
      color: #ffffff;
      text-transform: uppercase;
      margin: 1rem 0 0.8rem 0;
    }
    .stages {
        display: flex;
        gap: 1rem;
        position: relative;
        button {
            position: absolute;
            bottom: 0;
            right: 0;
            transform: translateY(100%);
            margin-top: 2.5rem;
            width: 20rem;
            height: 3.75rem;
            background: rgba(255, 255, 255, 0.1);
            font-weight: 700;
            font-size: 1.2rem;
            line-height: 1.45rem;
            font-family: 'Akrobat';
            display: flex;
            align-items: center;
            justify-content: center;
            text-transform: uppercase;
            color: #ffffff;
            &:hover {
                background: rgba(255, 255, 255, 0.2);
            }
        }
    }

    .grass-bg {
      position: absolute;
      left: -0rem;
      bottom: 0.05rem;
    }

    .systems {
      display: flex;
      gap: 1rem;
    }
  }
}
</style>
