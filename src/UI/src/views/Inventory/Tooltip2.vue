<template>
  <div
    class="item-menu"
    @click="$emit('close')"
    :style="{'top': `${tooltip.y}px`,'left': `${tooltip.x}px`}"
  >
    <div class="content">
      <div class="header">
        <div class="title">{{ tooltip.item ? loc(tooltip.item.getDisplayName()) : '' }}</div>
        <div class="rare">
          <h3 class="about">Rarely</h3>
          <h3 class="count">{{ 1 }}</h3>
        </div>
      </div>
      <div class="description">
   Description of the item
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
    drop(e) {
      e.stopPropagation()
      this.$emit('drop')
    },
    split(e) {
      e.stopPropagation()
      this.$emit('split')
    },
    use(e) {
      e.stopPropagation()
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
  top: 50%;
  left: 50%;

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
    }
    .rare {
      display: flex;
      align-items: center;
      gap: 1vh;

      h3.about {
        font-weight: 500;
        font-size: 1.7vh;
        color: #ff00d6;
        text-shadow: 0px 0px 14px rgba(255, 0, 214, 0.25);
      }

      h3.count {
        background: rgba(71, 44, 132, 0.05);
        border: 1px solid #301934 ;
        padding: 0.6vh;
        width: fit-content;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 1.34vh;
      }
    }
  }
  .description {
    font-weight: 400;
    font-size: 1.6vh;
    padding: 1.7vh;
    line-height: 120%;

    color: rgba(255, 255, 255, 0.25);
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

      border: 1px solid rgba(255, 255, 255, 0.1);

      .name {
        font-weight: 300;
        font-size: 12px;
      }

      &:hover {
        background: #3f414e;
        border: 1px solid rgba(255, 255, 255, 0.25);
      }
    }
  }
}
</style>
