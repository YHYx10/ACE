<template>
  <div class="hints">
    <div v-for="(item, key) of list" :key="key" class="hint">
      <img :src="item.img" :alt="item.img" />
      <div class="about">
        <div class="title">{{ item.title }}</div>
        <div class="subtitle">{{ item.subtitle }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import LeftMouse from './assets/ico/leftMouse.png'
import RightMouse from './assets/ico/rightMouse.png'
import WheelMouse from './assets/ico/rightMouse.png'
export default {
  data() {
    return {
      list: [
        {
          img: LeftMouse,
          title: 'Left mouse button ',
          subtitle: 'Turn of the camera',
        },
        {
          img: RightMouse,
          title: 'The right mouse button',
          subtitle: 'The angle of the camera',
        },
        {
          img: WheelMouse,
          title: 'Mouse Wheel ',
          subtitle: 'Close/go away',
        },
      ],
    }
  },
}
</script>

<style lang="scss" scoped>
.hints {
  display: flex;
  gap: 2.315vh;
  .hint {
    display: flex;
    img {
      width: 1.852vh;
      height: 2.87vh;
    }
    .about {
      display: flex;
      flex-direction: column;
      justify-content: space-around;
      align-items: flex-start;
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      margin-left: 1.296vh;
      .title {
        font-size: 0.833vh;
        line-height: 94%;
        display: flex;
        align-items: center;
        letter-spacing: 0.03em;
        color: rgba(255, 255, 255, 0.5);
      }
      .subtitle {
        font-size: 1.111vh;
        line-height: 94%;
        display: flex;
        align-items: center;
        letter-spacing: 0.03em;
        color: #ffffff;
      }
    }
  }
}
</style>
