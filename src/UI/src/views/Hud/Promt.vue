<template>
  <div class="promt">
    <div
      class="promt-item"
      v-for="(elem, index) in promptData.items"
      :key="index"
    >
      <div class="promt-key">{{ elem.key }}</div>
      <div class="promt-descr">{{ loc(elem.text) }}</div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  computed: {
    ...mapState("hud", ["promptData"]),
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss">
.promt {
  position: absolute;
  left: 50%;
  bottom: 38px;
  display: flex;
  align-items: flex-start;
  flex-direction: column;
  transform: translateX(-50%);

  &-item {
    display: flex;
    align-items: center;
    padding: 0 21px;
    position: relative;

    &::after {
      content: "";
      position: absolute;
      left: 0;
      top: 50%;
      transform: translateY(-50%);
      height: 21px;
      width: 1px;
      background: #ffffff;
    }

    &:not(:last-child) {
      margin-bottom: 16px;
    }
  }

  &-key {
    width: 45px;
    height: 45px;
    background: rgba(0, 0, 0, 0.4);
    border: 1px solid rgba(255, 255, 255, 0.09);
    border-radius: 5px;
    margin-right: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 24px;
    line-height: 29px;
    letter-spacing: 0.03em;
    color: #ffffff;
  }

  &-descr {
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 18px;
    line-height: 22px;
    display: flex;
    align-items: center;
    letter-spacing: 0.03em;
    color: #ffffff;
    display: flex;
    align-items: center;
    max-width: 213px;
    white-space: pre-wrap;
    text-shadow: 0px 0px 10px rgba(0, 0, 0, 0.35);
  }
}
</style>