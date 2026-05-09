<template>
  <div class="biginfo" v-show="biginfo !== null">
    <div class="biginfo_title">{{ loc("biginfo_1") }}</div>
    <hr />
    <div class="biginfo_text">{{ loc(biginfo) }}</div>
    <div class="biginfo_btn" @click="close">{{ loc("biginfo_2") }}</div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
export default {
  computed: {
    ...mapState("hud", ["biginfo"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    close() {
      window.mp.trigger("biginfo:hide");
    },
  },
};
</script>

<style lang="scss">
.biginfo {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background: rgba(12, 14, 20, 0.8);
  padding: 25px 49px 30px 50px;
  display: flex;
  align-items: center;
  flex-direction: column;

  &_title {
    font-family: "Akrobat";
    font-weight: 900;
    font-size: 16px;
    line-height: 19px;
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: 19px;
  }

  &_text {
    max-width: 247px;
    white-space: pre-wrap;
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 16px;
    line-height: 19px;
    text-align: center;
    color: #ffffff;
    margin-bottom: 24px;
  }

  &_btn {
    cursor: pointer;
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    box-shadow: 0px 0px 15px rgba(207, 16, 25, 0.8);
    display: flex;
    justify-content: center;
    align-items: center;
    width: 157px;
    height: 40px;
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 16px;
    line-height: 19px;
    text-transform: uppercase;
    color: #ffffff;
  }
}
/* .biginfo {
  background: linear-gradient(
      180deg,
      rgba(41, 255, 242, 0.3) 0%,
      rgba(41, 255, 242, 0) 100%
    ),
    linear-gradient(0deg, rgba(0, 0, 0, 0.9), rgba(0, 0, 0, 0.9));
  width: 40rem;
  height: 24rem;
  position: absolute;
  //border: 1px solid #000;
  top: 15rem;
  left: 30rem;
  text-transform: uppercase;
  padding: 3rem;
  color: #fff;
  hr {
    border-color: rgba(#fff, 0.5);
    margin: 1.5rem 0;
  }
  &_text {
    letter-spacing: 0.07rem;
  }
  &_title {
    font-size: 3rem;
    font-weight: bold;
    background: linear-gradient(
      180deg,
      #ffffff 0%,
      rgba(255, 255, 255, 0) 100%
    );
    text-align: center;
    //margin-top: 3rem;
    background-clip: text;
    -webkit-text-fill-color: transparent;
    letter-spacing: 0.1rem;
  }
  &_btn {
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: #b6d300;
    font-weight: bold;
    letter-spacing: 0.1rem;
    font-size: 1.5rem;
    width: 8rem;
    height: 3.5rem;
    position: absolute;
    bottom: -1.75rem;
    left: 16rem;
    box-shadow: 0 0 1.5rem 0 #b6d300;
    &:hover {
      transform: scale(1.02);
    }
  }
} */
</style>