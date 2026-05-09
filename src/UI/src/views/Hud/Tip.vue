<template>
  <!-- <div class="hud-tip"
       :style="{ '--rightOffset': this.rightOffset + '%', '--bottomOffset': this.bottomOffset + '%', '--width': this.width + '%' }">
    <div class="tip-background">
      <img class="tip-img" src="/img/hud/tip.png"/>
    </div>
    <div class="tip">
      <span class="tip__header">{{ loc('tip_0') }}</span>
      <span class="tip__text">{{ loc(tip.text) }}</span>  
    </div>
  </div> -->
  <div class="hud-tip">
    <img src="/img/hud/tip/i.svg" alt="" />
    <div>
      <span class="hud-tip_title">{{ loc("tip_0") }}</span>
      <span class="hud-tip_descr">{{ loc(tip.text) }}</span>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  computed: {
    ...mapState("hud", ["tip"]),
    ...mapGetters("localization", ["loc"]),
  },

  props: ["bottomOffset", "width", "rightOffset"],
};
</script>

<style lang='scss' scoped>
@keyframes fromLeft {
  from {
    transform: translateY(0%) translateX(-50%) scaleX(0);
  }
  to {
    transform: translateY(0%) translateX(0%) scaleX(1);
  }
}
.hud-tip {
  position: absolute;
  top: 399px;
  left: 29px;
  display: flex;
  align-items: center;
  background: rgba(0, 0, 0, 0.25);
  overflow: hidden;
  padding: 14px 41px 14px 15px;

  &::after {
    content: "";
    width: 170px;
    height: 170px;
    position: absolute;
    top: 50%;
    left: 0;
    transform: translate(-50%, -50%);
    border-radius: 50%;
    background: rgba(42, 152, 254, 0.5);
    filter: blur(106.766px);
  }

  img {
    width: 27px;
    height: 27px;
    margin-right: 14px;
  }

  div span {
    display: block;
    font-family: "Akrobat";
    text-transform: uppercase;
    font-weight: 700;
  }

  &_title {
    font-size: 16px;
    line-height: 19px;
    color: #2a98fe;
    margin-bottom: 3px;
  }

  &_descr {
    font-size: 16px;
    line-height: 19px;
    color: #ffffff;
    max-width: 100%;
    white-space: pre-wrap;
  }
}
</style>
