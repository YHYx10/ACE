<template>
  <div
    class="item-menu"
    :style="{ top: `${tooltip.y}px`, left: `${tooltip.x}px` }"
    @mouseleave="$emit('close')"
  >
    <div class="content">
      <div class="header">
        <div class="title">{{ loc(tooltip.item.getDisplayName()) }}</div>
        <div class="rare">
          <h3 class="count">{{ tooltip.item.count }} pcs /</h3>
          <h3 class="weight">{{(tooltip.item.config.Weight / 1000).toFixed(1)}} kg</h3>
        </div>
      </div>
      <div v-show="tooltip.item.count > 1" class="description">
        Total weight: <span class="green">  {{ ((tooltip.item.config.Weight / 1000) * tooltip.item.count).toFixed(2) }} kg </span>
      </div>
    </div>

    <div class="btn-list">
      <div @mousedown="use" class="item-btn">
        <UseSVG />
        <div class="name">use</div>
      </div>
      <div @mousedown="split" class="item-btn">
        <SplitSVG />
        <div class="name">split</div>
      </div>
      <div @mousedown="drop" class="item-btn">
        <DropSVG />
        <div class="name">Throw it away</div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import DropSVG from './componentsSVG/DropSVG.vue'
import SplitSVG from './componentsSVG/SplitSVG.vue'
import UseSVG from './componentsSVG/UseSVG.vue'

export default {
  props: ['tooltip', 'adress'],
  components: { UseSVG, SplitSVG, DropSVG },
  computed: {
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    drop() {
      this.$emit('drop')
    },
    split() {
      this.$emit('split')
    },
    use() {
      this.$emit('use')
    },
  },
}
</script>

<style lang="scss" scoped>
.item-menu {
  z-index: 99;
  width: 40vh;
  font-family: 'Akrobat';
  letter-spacing: .03vh;
  position: absolute;

  .content {
    background: rgba(255, 255, 255, 0.03);
    backdrop-filter: blur(1.6vh);
  }
  .header {
    padding: 0.6vh 1.6vh;
    display: flex;
    justify-content: space-between;
    background: linear-gradient(
      272deg,
      rgba(255, 255, 255, 0) 0%,
      rgba(255, 255, 255, 0.05) 100%
    );
    align-items: center;
    .title {
      font-weight: 500;
      font-size: 1.7vh;
      color: #ffffff;
      padding: 1vh;
    }
    .rare {
      display: flex;
      align-items: center;
      gap: 1vh;

      h3.weight {
        font-weight: 500;
        font-size: 1.7vh;
        color: rgb(92, 255, 128);
        text-shadow: 0px 0px 14px rgba(2, 18, 3, 0.25);
      }

      h3.count {
        width: fit-content;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 1.7vh;
      }
    }
  }
  .description {
    font-weight: 400;
    font-size: 1.6vh;
    padding: 1.7vh;
    line-height: 120%;
    color: rgba(255, 255, 255, 0.75);
    .green {
      color: rgb(92, 255, 128);
    }
  }

  .btn-list {
    display: flex;
    margin-top: 6px;
    gap: 6px;
    .item-btn {
      width: 100%;
      height: 85px;
      background: #22232c;
      display: flex;
      justify-content: center;
      align-items: center;
      flex-direction: column;
      gap: 10px;
      transition: 0.0s ease;
      position: relative;
      > * {
        z-index: 2;
      }

      .name {
        font-weight: 300;
        font-size: 12px;
      }
      &::before {
        content: '';
        position: absolute;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        background: linear-gradient(180deg, #301934  0%, #591b87 100%);
        opacity: 0;
        
        transition: 0.3s ease;
      }

      &:hover {
        &::before {
          opacity: 1;
        }
      }
    }
  }
}
</style>
