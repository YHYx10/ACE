<template>
  <div class="card" @click="$emit('click')" :class="{ active: active }">
    <div class="header">
      <div class="title">{{ title }}</div>
      <button
        @click="$emit('onBtnClick')"
        :class="{ border: buttonText, active: isBtnActive }"
      >
        <div v-if="buttonText">{{ buttonText }}</div>
        <svg
          v-else
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 27 26"
        >
          <path
            d="m14.09 19.538-.293.405.294-.405a1 1 0 0 0-1.176 0l-7.278 5.288 2.78-8.556a1 1 0 0 0-.363-1.117L.776 9.864h8.996a1 1 0 0 0 .951-.691l2.78-8.556 2.78 8.556a1 1 0 0 0 .951.69h8.996l-7.278 5.288a1 1 0 0 0-.363 1.118l2.78 8.556-7.278-5.288Z"
          />
        </svg>
      </button>
    </div>
    <img v-show="imgPath" :src="imgPath" loading="lazy" :alt="title" />
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    buttonText: String,
    isBtnActive: Boolean,
    active: Boolean,
    imgPath: [String, null],
  },
}
</script>

<style lang="scss" scoped>

.card {
  box-shadow: 0 0 0 0.093vh rgba(255, 255, 255, 0.49);
  background-color: black;
  border-radius: 1.481vh;
  position: relative;
  overflow: hidden;
  background-size: cover;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  font-size: 1.481vh;
  line-height: 2.222vh;
  &:hover {
    box-shadow: 0 0 0 0.093vh rgba(255, 255, 255, 0.808);
  }
  &.active {
    background-image: url(../assets/img/highlight.png);
    box-shadow: 0 0 0 0.093vh rgba(255, 255, 255, 1);
  }
  .header {
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
    padding: 0 1.389vh;
    top: 1.204vh;
    position: absolute;
    z-index: 1;
    color: #fff;
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 600;
    .title {
      width: 75%;
      white-space: normal;
    }
    button {
      background: transparent;
      display: flex;
      align-items: center;
      justify-content: center;
      font-size: 1.296vh;
      line-height: 2.222vh;
      text-align: center;
      color: #fff;
      svg {
        width: 2.5vh;
        stroke: #fff;
        &:hover {
          stroke: #301934 ;
        }
      }
      &.border {
        width: 2.407vh;
        height: 2.407vh;
        border: 0.093vh solid #ffffff;
        border-radius: 0.185vh;
        &:hover {
          border-color: #301934 ;
        }
      }
      &.active {
        svg {
          stroke: #301934 ;
          fill: #301934 ;
        }
        &.border {
          border-color: #301934 ;
          background: #301934 ;
        }
      }
    }
  }
  img {
    position: absolute;
    width: 100%;
    height: 100%;
    z-index: 0;
    object-fit: contain;
  }
}
</style>
