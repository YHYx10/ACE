<template>
  <div class="hud-radiospeaking">
    <div
      class="radiospeaking-item"
      v-for="(item, index) in speakItems"
      :key="index"
    >
      <img src="/img/hud/loudspeaker.svg" alt="loudspeaker" />
      <span>{{ item.name }}</span>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  computed: {
    ...mapState("hud", ["speakItems"]),
  },
};
</script>

<style lang="scss">
.hud-radiospeaking {
  position: absolute;
  right: 1.579rem;
  display: flex;
  flex-direction: column-reverse;
  align-items: flex-end;
  height: 10.947rem;
  bottom: 10rem;

  .radiospeaking-item {
    display: flex;
    align-items: center;
    background: rgba(0, 0, 0, 0.3);
    border-radius: 0.526rem;
    height: 2.105rem;
    padding: 0 0.421rem 0 0.684rem;

    &:not(:first-child) {
      margin-bottom: 0.105rem;
    }

    img {
      width: 0.684rem;
      height: 0.684rem;
      margin-right: 0.474rem;
    }

    span {
      font-family: "Akrobat";
      font-style: normal;
      font-weight: 700;
      font-size: 0.842rem;
      line-height: 1rem;
      text-transform: capitalize;
      color: #ffffff;
    }
  }
}
</style>