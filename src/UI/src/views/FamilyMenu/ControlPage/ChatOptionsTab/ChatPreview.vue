<template>
  <div class="chat-sample">
    <div class="message" :style="{ color: currentColor }">
      <span class="icon-wrap" :style="{ backgroundColor: currentColor }"
        ><span
          class="icon"
          :style="{
            backgroundImage:
              'url(/img/familyMenu/chatOptions/icon-' + currentIcon + '.svg)',
          }"
        ></span></span
      ><span class="nickname">[F] S_Wilson [16]: </span>
      <span class="message-text">Hallo!</span>
    </div>
    <div class="message" :style="{ color: currentColor }">
      <span class="icon-wrap" :style="{ backgroundColor: currentColor }"
        ><span
          class="icon"
          :style="{
            backgroundImage:
              'url(/img/familyMenu/chatOptions/icon-' + currentIcon + '.svg)',
          }"
        ></span></span
      ><span class="nickname">[F] A_Smith [66]: </span>
      <span class="message-text">Hallo!</span>
    </div>
    <div class="message" :style="{ color: currentColor }">
      <span class="icon-wrap" :style="{ backgroundColor: currentColor }"
        ><span
          class="icon"
          :style="{
            backgroundImage:
              'url(/img/familyMenu/chatOptions/icon-' + currentIcon + '.svg)',
          }"
        ></span></span
      ><span class="nickname">[F] N_Family [3]: </span>
      <span class="message-text">Willkommen!</span>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    currentColor: String,
    currentIcon: Number,
  }
}
</script>

<style lang="scss" scoped>
.chat-sample {
  display: flex;
  flex-flow: column;
  padding: 1.185vh 1.185vh 1.185vh 1.389vh;
  width: 100%;
  border: 0.093vh solid rgba(255, 255, 255, 0.09);
  .message {
    display: flex;
    align-items: center;
    justify-content: flex-start;
    white-space: pre;
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.222vh;
    margin-bottom: 1.111vh;
    &:last-child {
      margin-bottom: 0;
    }
    .icon-wrap {
      width: 2.778vh;
      height: 2.778vh;
      border-radius: 0.463vh;
      margin-right: 0.741vh;
      display: flex;
      align-items: center;
      justify-content: center;
      .icon {
        background-size: contain;
        background-position: center;
        background-repeat: no-repeat;
        width: 1.185vh;
        height: 1.185vh;
      }
    }
    .message-text {
      color: #fff;
      white-space: pre;
    }
  }
}
</style>
