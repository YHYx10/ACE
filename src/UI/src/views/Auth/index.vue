<template>
  <div class="auth">
    <div class="auth-tabs">
      <button
        class="auth-tab"
        v-for="(elem, index) in tabs"
        :key="index"
        :class="{ active: tabList[index] === currentTab }"
        @click="changeTab(index)"
      >
        {{ elem }}
      </button>
    </div>
    <div class="auth-content" :class="{ active: indexTab === 1 }">
      <CreateNewAccountTab v-if="currentTab == 'CreateNewAccountTab'" />
      <LoginTab v-if="currentTab == 'LoginTab'" />
      <PasswordRecovery v-if="currentTab == 'PasswordRecovery'" />
    </div>
    <img src="/img/newauths/auto.png" alt="" class="auth-auto" />
      <img src="/img/newauths/graph.png" alt="" />
      <div>
        <span>Night City RolepPlay</span>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div>
          <span>Night City<span>ROLEPLAY</span></span>
          <span
            >In this amazing server you can really immerse yourself in the process</span
          >
        </div>
      </div>
      <div>
        <span>???</span>
        <span>Don't have <span>Account?</span></span>
        <span>Fast <span>Register</span>, Not!</span>
      </div>
    </div>
</template>

<script>
import { mapState, mapMutations } from "vuex";
import CreateNewAccountTab from "./tabs/Reg.vue";
import LoginTab from "./tabs/Auth.vue";
import PasswordRecovery from "./tabs/PasswordRecovery.vue";

export default {
  data() {
    return {
      currentTabs: 0,
      tabs: ['Create user account', 'login', 'Forget password'],
      tabList: ["CreateNewAccountTab", "LoginTab", "PasswordRecovery"],
    };
  },
  components: { CreateNewAccountTab, LoginTab, PasswordRecovery },
  computed: {
    ...mapState("auth", ["socialClub", "currentTab"]),
  },
  methods: {
    ...mapMutations("auth", ["setCurrentTab"]),
    changeTab(idx) {
      if (this.currentTab === this.tabList[idx]) return;
      this.setCurrentTab({ page: this.tabList[idx] });
    },
  },
};
</script>

<style lang="scss">
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return $px / $height * 100vh;
  } @else {
    @return $px / $width * 100vw;
  }
}

.auth {
  width: 100%;
  height: 100%;
  position: relative;
  background: linear-gradient(270deg, rgba(0, 0, 0, 0.83) 0%, #010101 100%);
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: conv(86, true);

  * {
    font-family: "Akrobat";
  }

  &::after {
    content: "";
    position: absolute;
    bottom: conv(-407, true);
    right: conv(-561, true);
    width: conv(1377, true);
    height: conv(688, true);
    background: #040405;
    filter: blur(conv(278, true));
    border-radius: conv(688, true) / conv(344, true);
    z-index: 150;
  }

  &-tabs {
    display: flex;
    align-items: center;
    height: conv(61, true);
  }

  &-tab {
    cursor: pointer;
    border: none;
    border-bottom: conv(4, true) solid rgba(255, 255, 255, 0.05);
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
    font-weight: 700;
    font-size: conv(15, true);
    line-height: conv(18, true);
    text-transform: uppercase;
    color: rgba($color: #ffffff, $alpha: 0.25);
    background: none;
    position: relative;
    overflow: hidden;
    white-space: nowrap;

    &:first-child {
      width: conv(139, true);
    }
    &:nth-child(2) {
      width: conv(157, true);
    }
    &:last-child {
      width: conv(191, true);
    }

    &.active {
      border-color: #301934 ;
      color: #ffffff;

      &::after {
        content: "";
        position: absolute;
        left: calc(100% - conv(9));
        bottom: conv(2, true);
        width: conv(241);
        height: conv(53, true);
        background: #301934 ;
        filter: blur(conv(178, true));
        border-radius: conv(50, true) / conv(26, true);
      }
    }
  }

  .form {
    display: flex;
    align-items: flex-start;
    flex-direction: column;

    &-header {
      display: flex;
      flex-direction: column;
      span {
        min-width: auto;
      }
    }

    &-title {
      font-weight: 800;
      font-size: conv(48, true);
      line-height: conv(58, true);
      min-width: conv(300, false);
      background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0px 0px 112.591px rgba(255, 255, 255, 0.25);
    }

    &-descr {
      //margin-top: conv(-9, true);
      font-weight: 600;
      font-size: conv(15, true);
      line-height: conv(18, true);
      width: 27.778vh;
      text-transform: uppercase;
      color: #ffffff;
    }

    &-content {
      display: flex;
      flex-direction: column;
      margin-top: conv(25, true);
    }

    &-save {
      display: flex;
      align-items: center;
      margin: conv(12, true) 0 conv(16, true);

      span {
        font-weight: 700;
        font-size: conv(12, true);
        line-height: conv(14, true);
        text-transform: uppercase;
        color: #ffffff;
        margin-left: conv(13);
      }
    }

    & > div:not(.form-header),
    & > button {
      width: conv(300, true);
    }

    &-success {
      width: 100%;
      border: 1px solid #00ed31;
      padding: conv(14, true) conv(38) conv(14, true) conv(33);
      position: relative;
      overflow: hidden;
      margin-bottom: conv(10, true);

      span {
        display: block;
      }

      span:first-child {
        font-weight: 600;
        font-size: conv(14, true);
        line-height: conv(17, true);
        text-transform: uppercase;
        color: #ffffff;
      }

      span:last-child {
        font-weight: 700;
        font-size: conv(12, true);
        line-height: conv(14, true);
        text-transform: uppercase;
        color: rgba($color: #ffffff, $alpha: 0.55);
      }

      &::after {
        content: "";
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        pointer-events: none;
        width: conv(107, true);
        height: conv(107, true);
        bottom: conv(-87, true);
        background: #00941e;
        filter: blur(conv(80, true)); /* 178 */
        border-radius: 50%;
      }
    }

    &-error {
      width: 100%;
      border: 1px solid #301934 ;
      padding: conv(14, true) conv(38) conv(14, true) conv(33);
      position: relative;
      overflow: hidden;
      margin-bottom: conv(10, true);

      span {
        display: block;
      }

      span:first-child {
        font-weight: 600;
        font-size: conv(14, true);
        line-height: conv(17, true);
        text-transform: uppercase;
        color: #ffffff;
      }

      span:last-child {
        font-weight: 700;
        font-size: conv(12, true);
        line-height: conv(14, true);
        text-transform: uppercase;
        color: rgba($color: #ffffff, $alpha: 0.55);
      }

      &::after {
        content: "";
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        pointer-events: none;
        width: conv(107, true);
        height: conv(107, true);
        bottom: conv(-87, true);
        background: #301934 ;
        filter: blur(conv(80, true)); /* 178 */
        border-radius: 50%;
      }
    }

    &-btn {
      height: conv(66, true);
      display: flex;
      justify-content: center;
      align-items: center;
      font-weight: 600;
      font-size: conv(14, true);
      line-height: conv(17, true);
      text-transform: uppercase;
      color: #ffffff;
      // background: linear-gradient(180deg, #705DFF 0%, #591b87 100%);
      // box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
      border: none;
      cursor: pointer;

      // &:hover {
      //   background: linear-gradient(180deg, #c21019 0%, #66070c 100%);
      //   box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
      // }

      &:active {
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.25) 0%,
          rgba(75, 0, 130, 0.25) 100%
        );
        box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
      }
    }
  }

  &-content {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);

    /* &.active {
    padding-top: conv(173, true);
  } */
  }

  .form-input {
    width: 100%;
    height: conv(72, true);
    position: relative;
    overflow: hidden;
    display: flex;
    flex-direction: column;
    justify-content: center;
    padding-left: conv(33, true);

    span {
      display: block;
      font-weight: 700;
      font-size: conv(12, true);
      line-height: conv(14, true);
      text-transform: uppercase;
      color: rgba($color: #ffffff, $alpha: 0.25);
    }

    input {
      background: none;
      border: none;
      outline: none;

      &,
      &::placeholder {
        font-weight: 600;
        font-size: conv(15, true);
        line-height: conv(18, true);
        color: #ffffff;
      }

      &::placeholder {
        text-transform: uppercase;
      }
    }

    &::after {
      content: "";
      position: absolute;
      left: 50%;
      bottom: conv(-27, true);
      transform: translateX(-50%);
      width: conv(72, true);
      height: conv(72, true);
      background: rgba(255, 255, 255, 0.55);
      filter: blur(conv(100, true)); /* 178 */
      border-radius: 50%;
      pointer-events: none;
    }

    &::before {
      content: "";
      position: absolute;
      top: 50%;
      left: conv(14, true);
      transform: translateY(-50%);
      background: #ffffff;
      box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
      height: conv(26, true);
      width: conv(2, true);
    }

    &:not(:last-child) {
      margin-bottom: conv(10, true);
    }

    &.error {
    }
  }

  &-auto {
    position: absolute;
    bottom: conv(-60, true);
    right: conv(-150, true);
    height: conv(750, true);
    z-index: 100;
    pointer-events: none;
  }

  &-logo {
    position: absolute;
    left: 0;
    top: 50%;
    width: conv(743, true);
    height: conv(750, true);
    transform: translateY(-50%) scale(1);
    display: flex;
    justify-content: center;
    align-items: center;
    pointer-events: none;

    img:first-child {
      position: absolute;
      top: 50%;
      left: 0;
      transform: translateY(-50%) scale(1);
      animation: scales 1s infinite ease-in-out;
      animation-direction: alternate-reverse;
      width: auto;
      height: 100%;
    }

    img:nth-child(2) {
      position: absolute;
      top: conv(221.95, true);
      right: conv(27, true);
      width: conv(337.62, true);
      height: conv(349.8, true);
    }

    & > div {
      position: absolute;

      &:nth-child(3) {
        top: conv(147, true);
        right: conv(77.13, true);

        & > span {
          font-style: normal;
          font-weight: 800;
          font-size: conv(125, true);
          line-height: conv(136, true);
          letter-spacing: 0.01em;
          text-transform: uppercase;
          -webkit-text-stroke-width: 1px;
          -webkit-text-stroke-color: rgba(255, 255, 255, 0.25);
          color: transparent;
        }

        & > div {
          position: absolute;
          top: 60%;
          left: conv(55, true);
          transform: translate(0%, -50%);
          display: flex;
          flex-direction: column;
          width: conv(243, true);

          & > span {
            display: flex;
            align-items: center;

            &:first-child {
              font-weight: 800;
              font-size: 31.9541px;
              line-height: 38px;
              text-align: center;
              text-transform: uppercase;
              color: #ffffff;
            }

            &:last-child {
              font-weight: 600;
              font-size: conv(14, true);
              line-height: conv(17, true);
              text-transform: uppercase;
              color: #ffffff;
            }

            span {
              color: #301934 ;
            }
          }
        }
      }

      &:last-child {
        left: conv(113.73, true);
        bottom: conv(182.25, true);
        display: flex;
        flex-direction: column;

        & > span {
          &:first-child {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            font-weight: 800;
            font-size: conv(199.526, true);
            line-height: conv(239, true);
            letter-spacing: 0.01em;
            text-transform: uppercase;
            -webkit-text-stroke-width: 1px;
            -webkit-text-stroke-color: rgba(255, 255, 255, 0.25);
            color: transparent;
          }

          &:nth-child(2) {
            font-weight: 800;
            font-size: conv(29.903, true);
            line-height: conv(36, true);
            text-transform: uppercase;
            color: #ffffff;
            margin-left: conv(24.52, true);
          }

          &:nth-child(3) {
            font-weight: 600;
            font-size: conv(14, true);
            line-height: conv(17, true);
            text-transform: uppercase;
            color: #ffffff;

            span {
              text-decoration: underline;
              text-decoration-color: #301934 ;
            }
          }
          span {
            color: #301934 ;
          }
        }
      }
    }
  }

  @keyframes scales {
    0% {
      transform: translateY(-50%) scale(1);
    }
    100% {
      transform: translateY(-50%) scale(1.1);
    }
  }
}

/* @keyframes move {
  0% {
    transform: translateX(0);
  }
  100% {
    transform: translateX(-25px);
  }
} */
</style>