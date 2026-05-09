<template>
  <div class="body-tab">
    <div class="body-tab__interaction">
      <div class="body-tab__title">{{ loc("cityHallWeb_18") }}</div>
      <div class="body-tab__desc">{{ loc("cityHallWeb_19") }}</div>
      <div class="body-tab_bg">
        <div class="switch-org">
          <div
            :class="[{ active: currentOrg === 0 }, 'switch-org__item']"
            @click="setCurrentOrg(0)"
          >
            <div></div>
            <div class="switch-org_img">
              <img src="/img/cityHallWeb/donation/child.png" alt="" />
              <img src="/img/cityHallWeb/donation/child.png" alt="" />
            </div>
            <div>Orphanage<br />Home</div>
          </div>
          <div
            :class="[{ active: currentOrg === 1 }, 'switch-org__item']"
            @click="setCurrentOrg(1)"
          >
            <div></div>
            <div class="switch-org_img">
              <img src="/img/cityHallWeb/donation/older.png" alt="" />
              <img src="/img/cityHallWeb/donation/older.png" alt="" />
            </div>
            <div>
              Oldage<br />
        Home
            </div>
          </div>
        </div>
        <div class="input-wraps">
          <div class="desc">{{ loc("cityHallWeb_24") }}</div>
          <div class="pay-wrap">
            <div class="pay-wrap__input">
              <input type="number" v-model="currentPay" placeholder="0" />
            </div>
          </div>
        </div>

        <div class="btns" v-if="currentPay">
          <div class="pay bank-btn" @click="sendPayDonat">
            {{ loc("cityHallWeb_22") }}
          </div>
          <Payment
            :currentPayment="currentPayment"
            :setCurrentPayment="setCurrentPayment"
          />
        </div>
      </div>
    </div>
    <div class="body-tab__list">
      <div class="title">{{ loc("cityHallWeb_23") }}:</div>
      <div class="list-items">
        <div
          class="list-item"
          v-for="(item, index) in sortedArray"
          :key="item.key"
        >
          <span>{{ index + 1 }}</span>
          <div>{{ item.name }}</div>
          <div>${{ item.donate }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
import Payment from "../components/Payment";

export default {
  name: "DonationsTab",
  components: {
    Payment,
  },

  data: function () {
    return {
      currentOrg: 0,
      currentPay: null,
      currentPayment: 0,
    };
  },

  methods: {
    setCurrentOrg: function (value) {
      this.currentOrg = value;
    },
    sendPayDonat: function () {
      window.mp.trigger(
        "cityHallWeb:sendPayDonat",
        this.currentPay,
        this.currentOrg,
        this.currentPayment
      );
    },
    setCurrentPayment: function (value) {
      this.currentPayment = value;
    },
  },

  computed: {
    ...mapState("cityHallWeb/donations", ["list"]),
    ...mapState("cityHallWeb", ["cardId"]),
    ...mapGetters("localization", ["loc"]),
    sortedArray: function () {
      let array = [...this.list];
      return array.sort((a, b) => b.donate - a.donate);
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

@keyframes isShow {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

.city-hall-web {
  .body-tab {
    display: grid;
    grid-template-columns: conv(800) conv(815);
    column-gap: conv(20);

    &_title {
      margin-bottom: conv(7);
    }

    &_desc {
      margin-bottom: conv(45);
    }

    &_bg {
      width: 100%;
      height: 100%;
      background: rgba(0, 0, 0, 0.1);
      padding: conv(63) conv(65) 0;
      display: flex;
      flex-direction: column;
      align-items: center;
    }

    &__interaction {
      width: 100%;
      height: conv(700);
      min-height: conv(700);
      display: flex;
      flex-direction: column;

      .input-wraps {
        width: conv(342);
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-top: conv(19);
      }

      .desc {
        width: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        font-weight: 700;
        font-size: conv(20);
        line-height: conv(42);
        text-transform: uppercase;
        color: #ffffff;
      }

      .switch-org {
        display: grid;
        grid-template-columns: repeat(2, conv(325));
        height: conv(192);
        column-gap: conv(20);

        & > div {
          width: 100%;
          height: 100%;
        }
      }

      .switch-org {
        &__item {
          position: relative;
          background: rgba(255, 255, 255, 0.03);
          border: 1px solid rgba(255, 255, 255, 0.09);
          transition: 0.3s ease;
          display: flex;
          align-items: center;
          justify-content: flex-start;
          padding-left: conv(20);
          overflow: hidden;
          cursor: pointer;

          div:last-child {
            font-weight: 700;
            font-size: conv(24);
            line-height: conv(29);
            text-transform: uppercase;
            color: #ffffff;
            z-index: 6;
          }

          .switch-org_img {
            position: absolute;
            pointer-events: none;

            img {
              &:first-child {
                width: 100%;
                height: 100%;
                z-index: 5;
                position: relative;
              }

              &:last-child {
                width: 200%;
                height: 200%;
                position: absolute;
                opacity: 0.1;
                z-index: 3;
              }
            }
          }

          &::after,
          &::before {
            content: "";
            position: absolute;
            border-radius: 50%;
            pointer-events: none;
            z-index: 2;
            width: conv(442);
            height: conv(442);
            top: conv(-13);
            left: conv(20);
            transition: 0.2s ease;
          }

          & > div:first-child {
            content: "";
            z-index: 2;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            transition: 0.2s ease;
            opacity: 0;
            background: linear-gradient(
              180deg,
              #301934  0%,
              #591b87 100%,
              #591b87 100%
            );
          }

          &:first-child {
            &::after,
            &::before {
              background: radial-gradient(
                50% 50% at 50% 50%,
                rgba(71, 44, 132, 0.15) 0%,
                rgba(71, 44, 132, 0) 100%
              );
            }

            &::before {
              opacity: 0;
            }

            .switch-org_img {
              bottom: conv(-53);
              width: conv(136);
              height: conv(315);
              right: conv(24);

              img:last-child {
                right: conv(-1);
                bottom: conv(-85);
                width: conv(224);
                height: conv(519);
              }
            }
          }

          &:last-child {
            &::after,
            &::before {
              background: radial-gradient(
                50% 50% at 50% 50%,
                rgba(71, 44, 132, 0.15) 0%,
                rgba(71, 44, 132, 0) 100%
              );
            }

            &::before {
              opacity: 0;
            }

            .switch-org_img {
              bottom: conv(-89);
              width: conv(208);
              height: conv(276);
              right: conv(-10);

              img:last-child {
                right: conv(0);
                bottom: conv(-71);
                width: conv(315);
                height: conv(418);
              }
            }
          }

          &:hover {
            &::before {
              opacity: 1;
            }
          }

          &.active {
            & > div:first-child {
              opacity: 1;
            }
          }

          /* &:hover,
          &.active {
            color: #46555f;
            transition: 0.3s;
          }
          &.active {
            &:before,
            &:after {
              content: "";
              width: 0.1rem;
              height: 1rem;
              background: #e00b29;
              transition: 0.3s;
            }
            &:before {
              margin-right: 0.3rem;
            }
            &:after {
              margin-left: 0.3rem;
            }
          } */
        }
      }

      .pay-wrap {
        width: 100%;
        height: conv(89);

        &__input {
          width: 100%;
          height: 100%;
        }
        &__input {
          position: relative;
          display: flex;
          align-items: center;
          background: rgba(0, 0, 0, 0.2);
          padding-left: conv(20);

          input {
            width: 100%;
            padding: 0 conv(35);
            color: white;
            background: none;
            border: none;
            outline: none;

            &::-webkit-outer-spin-button,
            &::-webkit-inner-spin-button {
              -webkit-appearance: none;
              margin: 0;
            }
            &::placeholder {
              color: #46555f;
            }

            &::placeholder,
            & {
              font-weight: 700;
              font-size: conv(20);
              line-height: conv(24);
              text-transform: uppercase;
            }

            &::placeholder {
              color: rgba(255, 255, 255, 0.2);
            }
          }

          &:before {
            content: "$";
            position: absolute;
            left: conv(20);
            z-index: 0;
            font-weight: 700;
            font-size: conv(32);
            line-height: conv(38);
            display: flex;
            align-items: center;
            text-transform: uppercase;
            color: #ffffff;
          }
        }
      }

      .btns {
        width: conv(295);
        display: flex;
        align-items: center;
        flex-direction: column;

        .pay {
          background: linear-gradient(
            180deg,
            #301934  0%,
            #591b87 100%,
            #591b87 100%
          );
          cursor: pointer;
          width: 100%;
          height: conv(75);
          margin-bottom: conv(20);
          display: flex;
          align-items: center;
          justify-content: center;
          font-weight: 700;
          font-size: conv(24);
          line-height: conv(29);
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }
    &__list {
      padding-top: conv(20);
      width: 100%;

      .title {
        font-weight: 700;
        font-size: conv(40);
        line-height: conv(48);
        text-transform: uppercase;
        color: #ffffff;
        margin-bottom: conv(32);
      }

      .list-items {
        display: flex;
        flex-direction: column;
        max-height: conv(634);
        overflow-y: auto;
        width: 100%;
        padding-right: conv(10);

        &::-webkit-scrollbar {
          background: rgba(255, 255, 255, 0.05);
          width: conv(5);

          &-thumb {
            background: #301934 ;
          }
        }
      }

      .list-item {
        width: 100%;
        height: conv(88);
        min-height: conv(88);
        display: grid;
        grid-template-columns: 1fr auto;
        align-items: center;
        position: relative;
        background: rgba(0, 0, 0, 0.04);
        border: conv(1.05) solid rgba(255, 255, 255, 0.09);
        padding-right: conv(40);

        &:first-child {
          background: linear-gradient(
            90deg,
            rgba(255, 202, 66, 0.06) 0%,
            rgba(255, 202, 66, 0) 100%
          );
        }

        &:nth-child(2) {
          background: linear-gradient(
            90deg,
            rgba(255, 255, 255, 0.06) 0%,
            rgba(255, 255, 255, 0) 100%
          );
        }

        &:nth-child(3) {
          background: linear-gradient(
            90deg,
            rgba(255, 167, 103, 0.06) 0%,
            rgba(255, 167, 103, 0) 100%
          );
        }

        &:not(:last-child) {
          margin-bottom: conv(3);
        }

        span {
          position: absolute;
          top: 50%;
          right: conv(759);
          transform: translateY(-50%);
          font-weight: 700;
          font-size: conv(24);
          line-height: conv(29);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }

        div {
          &:nth-child(2) {
            width: 100%;
            padding-left: conv(95);
            position: relative;
            font-weight: 700;
            font-size: conv(24);
            line-height: conv(29);
            text-transform: uppercase;
            color: #ffffff;

            &::after {
              content: "";
              position: absolute;
              top: 50%;
              left: conv(65);
              height: conv(23);
              transform: translateY(-50%);
              width: conv(2);
              background: #ffffff;
              border: conv(1.04545) solid rgba(255, 255, 255, 0.09);
              box-shadow: 0px 0px conv(14.6364) rgba(255, 255, 255, 0.55);
            }
          }

          &:last-child {
            font-weight: 700;
            font-size: conv(24);
            line-height: conv(29);
            text-align: right;
            text-transform: uppercase;
            color: #ffffff;
          }
        }
      }
    }
  }
}
</style>
