<template>
  <div class="robshop-info">
    <div class="robshop-info_line"></div>
    <div class="robshop-info_btn">F</div>
    <div class="robshop-info_title">{{ loc("robshop:info:title") }}</div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss">
.robshop-info {
  margin-top: 1rem;
  display: flex;
  align-items: center;
  height: 3.526rem;
  width: 100%;

  &_line {
    width: 0.053rem;
    height: 1.421rem;
    background: #ffffff;
  }

  &_btn {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 2.368rem;
    height: 2.368rem;
    background: rgba(0, 0, 0, 0.4);
    border: 0.053rem solid rgba(255, 255, 255, 0.09);
    border-radius: 0.263rem;
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 1.263rem;
    /* line-height: 29px; */
    letter-spacing: 0.03em;
    color: #ffffff;
    margin: 0 1.053rem;
  }

  &_title {
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 0.947rem;
    line-height: 1.158rem;
    letter-spacing: 0.03em;
    color: #ffffff;
  }
}
</style>