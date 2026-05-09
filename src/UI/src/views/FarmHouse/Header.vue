<template>
  <div class="farm-house_header">
    <div class="farm-house_acc">
      <div class="farm-house_acc-logo">
        <img src="/img/farmHouse/logo.png" alt="" />
      </div>
      <div class="farm-house_acc-info">
        <div>The name of the farmer</div>
        <div>{{ nickname }}</div>
        <div class="farm-house_acc-lvl">
          <div>
            {{ userLevel.level }}
          </div>
          <div>
            <div class="progress">
              <div
                class="line"
                :style="{ width: `${currentLevelPercent}%` }"
              ></div>
            </div>
            <div>
              <div>LVL</div>
              <div>{{ currentExp }}/{{ userLevel.exp }} EXP</div>
            </div>
          </div>
          <div>{{ userLevel.level + 1 }}</div>
        </div>
      </div>
    </div>
    <div class="farm-house_btns">
      <!-- <button class="sell bank-btn" @click="$emit('click-sell')">
        {{ currentPage === "Sell" ? "Выйти с продажи" : "ПРОДАТЬ РЕСУРСЫ" }}
      </button> -->
      <button class="help" @click="$emit('click-help')"><div>Help</div></button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";

export default {
  props: {
    currentPage: String,
  },
  computed: {
    ...mapState("farmHouse", ["nickname", "levelsList", "currentExp"]),
    ...mapGetters("localization", ["loc"]),
    ...mapGetters("farmHouse", ["userLevel"]),
    currentLevelPercent: function () {
      return Math.floor((this.currentExp / this.userLevel.exp) * 100);
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house {
  &_header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    padding: 0 conv(90) 0 conv(35);
    margin-bottom: conv(36);
  }

  &_acc {
    display: flex;
    align-items: center;

    &-logo {
      border-radius: 50%;
      width: conv(96);
      height: conv(96);
      margin-right: conv(30);
      overflow: hidden;

      img {
        width: 100%;
        height: 100%;
      }
    }

    &-info {
      display: flex;
      flex-direction: column;

      & > div {
        &:first-child {
          margin-bottom: conv(3);
          font-weight: 600;
          font-size: conv(20);
          line-height: conv(24);
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.5);
        }

        &:nth-child(2) {
          font-weight: 700;
          font-size: conv(32);
          line-height: conv(38);
          text-transform: uppercase;
          color: #ffffff;
          margin-bottom: conv(9);
        }
      }
    }

    &-lvl {
      display: flex;

      & > div {
        &:first-child {
          font-weight: 700;
          font-size: conv(24);
          line-height: conv(29);
          text-transform: uppercase;
          color: #ffffff;
          margin-right: conv(15);
          margin-top: conv(5);
        }

        &:last-child {
          margin-left: conv(15);
          font-weight: 700;
          font-size: conv(32);
          line-height: conv(38);
          text-transform: uppercase;
          color: #ffca42;
          text-shadow: 0px 0px conv(20) rgba(255, 202, 66, 0.5);
        }

        &:nth-child(2) {
          display: flex;
          flex-direction: column;
          width: conv(364);
          margin-top: conv(12);

          & > div:last-child {
            display: flex;
            margin-top: conv(5);
            justify-content: space-between;
            align-items: center;

            div {
              font-weight: 700;
              font-size: conv(18);
              line-height: conv(22);
              text-transform: uppercase;
              color: rgba(255, 255, 255, 0.5);
            }
          }
        }
      }

      .progress {
        position: relative;
        overflow: hidden;
        width: 100%;
        height: conv(15);
        background: rgba(255, 255, 255, 0.07);
        border-radius: conv(20);

        .line {
          position: absolute;
          top: 0;
          left: 0;
          height: 100%;
          background: #301934 ;
          border-radius: conv(20);
        }
      }
    }
  }

  &_btns {
    display: flex;
    align-items: center;

    button {
      display: flex;
      justify-content: center;
      align-items: center;
      transition: 0.2s ease;
      width: conv(350);
      height: conv(75);
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;

      &:not(.active) {
        cursor: pointer;
      }

      &.active {
        pointer-events: none;
      }

      &:not(:last-child) {
        margin-right: conv(92);
      }

      &.sell {
        background: linear-gradient(
          180deg,
          #301934  0%,
          #591b87 100%,
          #591b87 100%
        );
      }

      &.help {
        background: rgba(255, 255, 255, 0.1);
        position: relative;
        transition: 0.1s linear;

        // &::after {
        //   content: "";
        //   transition: 0.2s ease;
        //   position: absolute;
        //   top: 0;
        //   left: 0;
        //   width: 100%;
        //   height: 100%;
        //   z-index: 1;
        //   opacity: 0;
        //   background: rgba(255, 255, 255, 0.1);
        // }

        &:hover {
          background: rgba(255, 255, 255, 0.15);
        }
      }
    }
  }
}
</style>