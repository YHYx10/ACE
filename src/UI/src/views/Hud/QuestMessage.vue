<template>
  <div class="questmsg" v-show="show && !promptData.show">
    <!-- <img src="/img/hud/questMessage/bg.png" alt="marker" class="questmsg_bg"> -->
    <span class="questmsg-title">{{ loc(title) }}</span>
    <div>
      <div class="questmsg-descr">
        <span>{{ loc(subtitle) }}</span>
        <svg
          width="13"
          height="13"
          viewBox="0 0 13 13"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          class="active"
        >
          <path
            d="M6.5 0C2.91027 0 0 2.91027 0 6.5C0 10.0897 2.91027 13 6.5 13C10.0897 13 13 10.0897 13 6.5C13 2.91027 10.0897 0 6.5 0ZM6.5 11.2667C3.87227 11.2667 1.73333 9.1286 1.73333 6.5C1.73333 3.8714 3.87227 1.73333 6.5 1.73333C9.12773 1.73333 11.2667 3.8714 11.2667 6.5C11.2667 9.1286 9.12773 11.2667 6.5 11.2667Z"
            fill="#301934 "
          />
          <path
            d="M6.5 10C8.433 10 10 8.433 10 6.5C10 4.567 8.433 3 6.5 3C4.567 3 3 4.567 3 6.5C3 8.433 4.567 10 6.5 10Z"
            fill="#301934 "
          />
        </svg>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
export default {
  computed: {
    ...mapState("hudQuestMessage", ["show", "title", "subtitle", "background"]),
    ...mapState("hud", ["promptData"]),
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss">
.questmsg {
  position: absolute;
  top: 10.263rem;
  right: 1.211rem;
  display: flex;
  align-items: flex-end;
  flex-direction: column;
  width: 14.158rem;

  &::after {
    content: "";
    position: absolute;
    width: 12.632rem;
    height: 5.053rem;
    right: -6.579rem;
    bottom: 0.053rem;
    background: #000000;
    filter: blur(2.632rem);
    z-index: -1;
    border-radius: 50%;
  }

  span {
    display: block;
  }

  &-title {
    display: block;
    font-family: "Akrobat";
    font-weight: 900;
    font-size: 0.842rem;
    line-height: 1rem;
    display: flex;
    align-items: center;
    text-align: right;
    text-transform: uppercase;
    color: #ffffff;
    text-shadow: 0px 2px 5px rgba(0, 0, 0, 0.25);
    margin-right: 1.211rem;
    margin-bottom: 0.526rem;
  }

  &-descr {
    display: flex;
    justify-content: flex-end;
    align-items: center;

    span {
      position: relative;
      font-family: "Akrobat";
      font-weight: 700;
      font-size: 0.737rem;
      line-height: 0.895rem;
      text-align: right;
      text-transform: uppercase;
      color: #ffffff;
      text-shadow: 0px 2px 5px rgba(0, 0, 0, 0.25);
      max-width: 10.526rem;
    }

    svg {
      height: 0.684rem;
      width: 0.684rem;
      opacity: 0.2;
      margin-left: 0.526rem;

      &.active {
        opacity: 1;
      }
    }
  }
}
</style>