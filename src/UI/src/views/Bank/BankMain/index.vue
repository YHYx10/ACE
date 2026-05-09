<template>
  <div class="bank-main">
    <div class="bank-main_card">
      <div class="bank-main_title">Your card</div>
      <div class="bank-main_card-wrap">
        <div class="bank-main_cards">
          <div class="bank-main_cards-check">№ Content:&nbsp;{{ account }}</div>
          <div class="bank-main_cards-balance">
            $ {{ balance.toLocaleString("ru") }}
          </div>
          <div class="bank-main_cards-text">Card number</div>
          <div class="bank-main_cards-number">{{ cardNumber }}</div>
        </div>

        <div class="bank-main_services">
          <button
            class="bank-main_services-item"
            v-for="(elem, index) in [
              ['Fill the balance', 'TopUp', 'topup'],
              ['Withdraw', 'Withdraw', 'widthdraw'],
              ['Transfer to another account', 'Transfer', 'transfer'],
            ]"
            :key="index"
            @click="
              $emit('setCurrentModal', {
                component: elem[1],
                dataModal: {
                  type: 'personal',
                  balance: balance,
                },
              })
            "
          >
            <img :src="`/img/bank/main/${elem[2]}.svg`" alt="" />
            <div class="bank-main_services-text">{{ elem[0] }}</div>
          </button>
        </div>
      </div>
    </div>

    <div class="bank-main_balance">
      <div class="bank-main_title">Payments</div>

      <div class="bank-main_balance-wrap">
        <div class="bank-main_balance-info">
          <div>
            <div
              v-for="(elem, index) in info"
              :key="index"
              @click="() => setModal(elem.modal)"
            >
              <div>
                <img :src="`/img/bank/main/${elem.img}.png`" alt="" />
                <img :src="`/img/bank/main/${elem.img}.png`" alt="" />
              </div>
              <div>
                <div>{{ elem.title }}</div>
                <div>{{ elem.descr }}</div>
              </div>
            </div>
          </div>
          <div class="bank-main_balance-charity">
            <div>
              <img src="/img/bank/main/charity.png" alt="" />
              <img src="/img/bank/main/charity.png" alt="" />
            </div>

            <div>
              <div>Charity</div>
              <div>Help those who are in need </div>
            </div>

            <div>
              <input
                type="number"
                placeholder="Enter the amount"
                v-model="currentCharity"
              />
              <button @click="donateCharity" class="bank-btn">Send</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="bank-main_transaction">
      <div class="bank-main_title">Your transactions</div>

      <div class="bank-main_transaction-wrap">
        <div
          class="bank-main_transaction-item"
          v-for="(item, index) in getTransactions"
          :key="index"
        >
          <div :class="item.directFrom ? 'low' : 'high'">
            <img
              :src="`/img/bank/icon/${item.directFrom ? 'low' : 'high'}.png`"
              alt=""
            />
          </div>
          <div>{{item.directFrom ? '-' : '+'}} &nbsp;${{ getAmount(item) }}</div>
          <div>{{ loc(item.comment) }}</div>
        </div>
      </div>
    </div>

    <transition name="bank-modal">
      <component
        v-if="currentModal"
        :is="currentModal"
        @closeModal="closeModal"
      />
    </transition>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
import PayForHouse from "./modals/BankMainPayForHouse";
import Mobile from "./modals/BankMainMobile";
import Fines from "./modals/BankMainFines";
export default {
  name: "BankMain",
  components: {
    PayForHouse,
    Mobile,
    Fines,
  },
  data() {
    return {
      currentCharity: null,
      currentModal: null,
      info: [
        {
          modal: "PayForHouse",
          img: "house",
          title: "Payment for the house",
          descr: "Pay the property tax without commission",
        },
        {
          modal: "PayForBiz",
          img: "biz",
          title: "Pay for your company",
          descr: "Pay punctual business tax",
        },
        {
          modal: "Fines",
          img: "fines",
          title: "Payment of fines",
          descr: "Don't forget to pay all fines in good time ",
        },
        {
          modal: "Mobile",
          img: "mobile",
          title: "Mobile communication",
          descr: "Immediate filling of the balance of every operator",
        },
      ],
    };
  },
  computed: {
    ...mapState("smartphone/bankPage", ["balance"]),
    ...mapState("bank", ["account", "cardNumber", "transactionsList"]),
    ...mapGetters("localization", ["loc"]),
    getTransactions() {
      return this.transactionsList.slice().sort((a, b) => b.date - a.date);
    },
  },
  methods: {
    setModal(name) {
      if (name === "PayForBiz") {
        this.$emit("setCurrentModal", {
          component: "PayForBiz",
          dataModal: {
            type: "business",
          },
        });
      } else {
        this.setCurrentModal(name);
      }
    },
    donateCharity() {
      if (this.currentCharity > 0) {
        window.mp.triggerServer(
          "government:donateToGov",
          this.currentCharity,
          0,
          false
        );
        this.currentCharity = null;
      }
    },
    setCurrentModal(value) {
      this.currentModal = value;
    },
    closeModal() {
      this.currentModal = null;
    },
    getAmount(transact) {
      if (transact.directFrom)
        return `${transact.value.toLocaleString("ru")}`;
      else return `${(transact.value - transact.tax).toLocaleString("ru")}`;
    },
  },
};
</script>

<style lang="scss">
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / 20) + rem;
  } @else {
    @return ($px / 20) + rem;
  }
}

.bank-main {
  padding: conv(79, true) conv(137) conv(158, true) conv(142);
  display: grid !important;
  grid-template-columns: conv(405) 1fr conv(382);
  column-gap: conv(50);
  width: 100%;
  height: 100%;

  & > div {
    width: 100%;
    height: 100%;
  }

  &_title {
    font-weight: 700;
    font-size: conv(32, true);
    line-height: conv(38, true);
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: conv(24, true);
  }

  &_card {
    display: flex;
    flex-direction: column;

    &-wrap {
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      height: 100%;
    }
  }

  &_cards {
    background: url(/img/bank/main/card.png) center center no-repeat;
    background-size: 100% 100%;
    /* height: conv(240, true); */
    width: 100%;
    padding: conv(20, true) 0 conv(13, true) conv(32);

    div {
      text-transform: uppercase;
      font-weight: 700;
    }

    &-check,
    &-text {
      font-size: conv(20, true);
      line-height: conv(24, true);
    }

    &-check {
      color: #b8c6c9;
    }

    &-balance {
      margin: conv(24, true) 0 conv(30, true);
      padding-left: conv(76);
      font-size: conv(50, true);
      line-height: conv(60, true);
      color: #ffffff;
    }

    &-text {
      color: rgba(255, 255, 255, 0.5);
    }

    &-number {
      margin-top: conv(2, true);
      font-size: conv(36, true);
      line-height: conv(43, true);
      color: #b8c6c9;
    }
  }

  &_services {
    display: flex;
    flex-direction: column;
    width: 100%;

    &-item {
      width: 100%;
      height: conv(73, true);
      background: rgba(255, 255, 255, 0.04);
      display: flex;
      align-items: center;
      cursor: pointer;
      transition: 0.3s ease;
      border: none;
      outline: none;
      white-space: nowrap;

      &:first-child {
        padding: 0 conv(29);

        img {
          margin-right: conv(17);
          width: conv(29, true);
          height: conv(29, true);
        }
      }

      &:nth-child(2) {
        padding: 0 conv(27);

        img {
          margin-right: conv(17);
          width: conv(31, true);
          height: conv(31, true);
        }
      }

      &:nth-child(3) {
        padding: 0 conv(25);

        img {
          margin-right: conv(15);
          width: conv(35, true);
          height: conv(35, true);
        }
      }

      &:nth-child(4) {
        padding: 0 conv(25);

        img {
          margin-right: conv(15);
          width: conv(35, true);
          height: conv(35, true);
        }
      }

      div {
        font-weight: 700;
        font-size: conv(20, true);
        line-height: conv(24, true);
        text-transform: uppercase;
        color: #ffffff;
      }

      &:not(:last-child) {
        margin-bottom: conv(10, true);
      }

      &:hover {
        background: rgba(255, 255, 255, 0.1);
      }
    }
  }

  &_balance {
    display: flex;
    flex-direction: column;

    &-wrap {
      height: 100%;
      width: 100%;
      display: flex;
    }

    &-info {
      display: grid;
      grid-template-rows: 1fr conv(150, true);
      row-gap: conv(10, true);
      width: 100%;

      & > div:first-child {
        display: grid;
        grid-template-columns: 1fr 1fr;
        grid-template-rows: 1fr 1fr;
        row-gap: conv(10, true);
        column-gap: conv(10);

        & > div {
          width: 100%;
          height: 100%;
          background: rgba(255, 255, 255, 0.04);
          display: flex;
          align-items: center;
          position: relative;
          overflow: hidden;
          transition: 0.3s linear;

          &:first-child {
            padding-left: conv(20);

            & > div:first-child {
              height: conv(74, true);
              width: conv(74, true);
              margin-right: conv(25);
            }
          }

          &:nth-child(2) {
            padding-left: conv(27);

            & > div:first-child {
              height: conv(70, true);
              width: conv(69, true);
              margin-right: conv(23);
            }
          }

          &:nth-child(3) {
            padding-left: conv(31);

            & > div:first-child {
              height: conv(60, true);
              width: conv(60, true);
              margin-right: conv(28);
            }
          }

          &:last-child {
            & > div:first-child {
              height: conv(65, true);
              width: conv(85, true);
              margin-right: conv(15);
              transform: matrix(-1, 0, 0, 1, 0, 0);
            }
          }

          & > div {
            &:first-child {
              position: relative;

              img {
                &:first-child {
                  position: relative;
                  width: 100%;
                  height: 100%;
                  z-index: 2;
                }

                &:last-child {
                  position: absolute;
                  top: 50%;
                  left: 50%;
                  width: 200%;
                  height: 200%;
                  transform: translate(-50%, -50%);
                  z-index: 1;
                  opacity: 0.07;
                }
              }
            }

            &:last-child {
              z-index: 3;

              div {
                text-transform: uppercase;
                font-weight: 700;
              }

              div:first-child {
                font-size: conv(32, true);
                line-height: conv(38, true);
                color: #ffffff;
                margin-bottom: conv(2, true);
              }

              div:last-child {
                max-width: conv(194);
                font-size: conv(12, true);
                line-height: conv(14, true);
                color: rgba(255, 255, 255, 0.55);
              }
            }
          }

          &:hover {
            background: rgba(255, 255, 255, 0.07);
          }
        }
      }
    }

    &-charity {
      width: 100%;
      height: 100%;
      background: rgba(255, 255, 255, 0.02);
      padding: conv(19, true) conv(44);
      display: grid;
      grid-template-columns: conv(67, true) 1fr conv(204);
      align-items: center;

      & > div:first-child {
        width: conv(67, true);
        height: conv(67, true);
        position: relative;

        img {
          &:first-child {
            width: 100%;
            height: 100%;
            position: relative;
            z-index: 2;
          }

          &:last-child {
            position: absolute;
            z-index: 1;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 200%;
            height: 200%;
            opacity: 0.07;
          }
        }
      }

      & > div:nth-child(2) {
        padding-left: conv(45);

        div:first-child {
          font-weight: 700;
          font-size: conv(32, true);
          line-height: conv(38, true);
          text-transform: uppercase;
          color: #ffffff;
          margin-bottom: conv(2, true);
        }

        div:last-child {
          font-weight: 700;
          font-size: conv(12, true);
          line-height: conv(14, true);
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.55);
          max-width: conv(217);
        }
      }

      & > div:last-child {
        display: grid;
        grid-template-rows: 1fr 1fr;
        width: 100%;
        row-gap: conv(6, true);
        height: 100%;

        input {
          display: flex;
          align-items: center;
          justify-content: center;
          background: rgba(255, 255, 255, 0.04);
          border: none;
          outline: none;
          color: white;

          &,
          &::placeholder {
            font-weight: 500;
            font-size: conv(14, true);
            line-height: conv(17, true);
            text-align: center;
            text-transform: uppercase;
            color: rgba(255, 255, 255, 0.5);
          }
        }

        button {
          border: none;
          outline: none;
          width: 100%;
          height: 100%;
          display: flex;
          align-items: center;
          justify-content: center;
          // background: linear-gradient(180deg, #db121b 0%, #591b87 100%);
          font-weight: 700;
          font-size: conv(20, true);
          line-height: conv(24, true);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
          cursor: pointer;
        }
      }
    }
  }

  &_transaction {
    &-wrap {
      height: 100%;
      max-height: 100%;
      display: flex;
      flex-direction: column;
      width: 100%;
      max-height: conv(576, true);
      overflow-x: hidden;
      overflow-y: auto;

      &::-webkit-scrollbar {
        width: 0.28vh;
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.23);
      }
      &::-webkit-scrollbar-thumb {
        background: #ff0000;
      }
    }

    &-item {
      width: 100%;
      height: conv(72, true);
      min-height: conv(72, true);
      background: rgba(255, 255, 255, 0.02);
      display: grid;
      align-items: center;
      grid-template-columns: conv(76, true) 3fr 2fr;
      column-gap: conv(21);
      border: 1px solid;
      padding-right: conv(10);
      border-image-source: linear-gradient(
        90deg,
        rgba(255, 255, 255, 0.09) 0%,
        rgba(255, 255, 255, 0) 101.25%
      );
      border-image-slice: 1;

      & > div:first-child {
        height: 100%;
        width: 100%;
        background: linear-gradient(
          90deg,
          rgba(160, 255, 152, 0.1) 0%,
          rgba(160, 255, 152, 0) 100%
        );
        display: flex;
        align-items: center;

        &.low {
          background: linear-gradient(
            90deg,
            rgba(255, 125, 125, 0.1) 0%,
            rgba(255, 125, 125, 0) 100%
          );
        }

        img {
          margin-left: conv(29);
          height: conv(34, true);
          width: conv(24, true);
        }
      }

      & > div:nth-child(2) {
        font-weight: 700;
        font-size: conv(20, true);
        line-height: conv(24, true);
        text-transform: uppercase;
        color: #ffffff;
      }

      & > div:last-child {
        font-weight: 700;
        font-size: conv(16, true);
        line-height: conv(19, true);
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.5);
      }
    }
  }
}

.bank-biz,
.bank-org,
.bank-family {
  .bank-main_transaction-wrap {
    max-height: conv(672, true);
  }
}

/* .bank-main {
  display: flex;
  & > div {
    display: flex;
    flex-direction: column;
    height: 100%;
    position: relative;
    &:nth-child(1) {
      width: 20rem;
      min-width: 20rem;
      margin-right: 7rem;
      &:after {
        content: "";
        width: 1.5rem;
        height: 0.75rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        background-image: url("/img/bank/arrow-light.png");
        position: absolute;
        right: -2.25rem;
        top: 5rem;
        transform: translateX(100%);
      }
    }
    &:nth-child(2) {
      width: 100%;
    }
  }
  &-modal {
    position: fixed !important;
    width: 100vw !important;
    height: 100vh !important;
    left: 0;
    top: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    // background: radial-gradient(43.85% 77.96% at 50% 50%, #5E37B0 0%, rgba(94, 55, 176, 0) 100%), rgba(0, 0, 0, 0.9);
    background: rgba(0, 0, 0, 0.6);
    &__wrap {
      width: 24rem;
      padding: 3rem;
      background: #ffffff;
      border-radius: 0.8rem;
      display: flex;
      flex-direction: column;
      position: relative;
    }
    &-title {
      font-weight: bold;
      font-size: 2.5rem;
      line-height: 2.5rem;
      letter-spacing: 0.03em;
      color: #000000;
    }
    &-desc,
    &-prompt {
      font-weight: normal;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      color: rgba(0, 0, 0, 0.4);
    }
    &-desc {
      margin-bottom: 1.5rem;
    }
    &-prompt {
      margin-bottom: 0.3rem;
      display: flex;
      align-items: center;
      &.card {
        &:before {
          content: "";
          width: 1.6rem;
          height: 1.6rem;
          margin-right: 0.5rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          background-image: url("/img/bank/card-icon-gray.png");
        }
      }
    }
    &__block-shadow {
      background: #ffffff;
      box-shadow: 0 1.5rem 2.5rem rgba(49, 41, 184, 0.1);
      border-radius: 0.5rem;
      padding: 0.85rem 1.5rem;
      position: relative;
      &:before {
        width: 1.6rem;
        height: 1.6rem;
        position: absolute;
        left: 1.5rem;
        top: 0.9rem;
        margin-right: 0.5rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
      }
      &.house,
      &.cash,
      &.biz {
        &:before {
          content: "";
        }
        padding-left: 3.85rem;
      }
      &.house {
        &:before {
          background-image: url("/img/bank/house-icon.png");
        }
      }
      &.cash {
        &:before {
          background-image: url("/img/bank/card-icon.png");
        }
      }
      &.biz {
        &:before {
          background-image: url("/img/bank/biz-icon.png");
        }
      }
    }
    &-btn {
      height: 3rem;
      border-radius: 0.5rem;
    }
    &__input {
      position: relative;
      width: 100%;
      padding-bottom: 0.5rem;
      border-bottom: 1px solid rgba(0, 0, 0, 0.1);
      display: flex;
      align-items: center;
      justify-content: flex-start;
      font-weight: bold;
      font-size: 2rem;
      line-height: 2rem;
      letter-spacing: 0.03em;
      color: #000000;
      margin-bottom: 1.2rem;
      input {
        width: 100%;
        font-weight: bold;
        font-size: 2rem;
        line-height: 2rem;
        letter-spacing: 0.03em;
        color: #000000;
        background-color: transparent;
        border: none;
        outline: none;
        &::-webkit-outer-spin-button,
        &::-webkit-inner-spin-button {
          //display: none;
          -webkit-appearance: none;
        }
        &::placeholder {
          font-weight: normal;
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          color: rgba(0, 0, 0, 0.4);
        }
      }
    }
  }
  &__card {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
    position: relative;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
    background-image: url("/img/bank/bg-card.png");
    padding: 1rem;
    width: 100%;
    height: 11rem;
    text-shadow: 0 0.1rem 0.15rem rgba(0, 0, 0, 0.25);
    margin-bottom: 1rem;
    &-account {
      color: #316e76;
      font-size: 0.9rem;
      line-height: 1.1rem;
      letter-spacing: 0.2em;
    }
    &-logo {
      width: 2.75rem;
      height: 0.75rem;
      position: absolute;
      right: 1rem;
      top: 1rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url("/img/bank/logo.png");
    }
    &-pay {
      width: 1.05rem;
      height: 1.3rem;
      position: absolute;
      right: 1.2rem;
      bottom: 1.2rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url("/img/bank/pay.png");
    }
    &-info {
      display: flex;
      flex-direction: column;
      font-weight: normal;
      letter-spacing: 0.3em;
      &-desc {
        font-size: 0.9rem;
        line-height: 1.1rem;
      }
      &-value {
        font-size: 2rem;
        line-height: 2.4rem;
      }
    }
  }
  &__balance {
    display: flex;
    align-items: center;
    padding: 0 0 0.5rem 0.7rem;
    letter-spacing: 0.03em;
    border-bottom: 0.1rem solid rgba(255, 255, 255, 0.14);
    margin-bottom: 1.4rem;
    width: 100%;
    &-desc {
      font-weight: normal;
      font-size: 1.2rem;
      line-height: 1.45rem;
      margin-right: 1rem;
    }
    &-value {
      font-weight: bold;
      font-size: 3rem;
    }
  }
  &__transactions {
    display: flex;
    flex-direction: column;
    height: 26rem;
    padding: 2rem 1.75rem 0 0;
    background: #ffffff;
    box-shadow: 0 1rem 4.5rem #0a041e;
    border-radius: 1.1rem;
    &-title {
      font-size: 2rem;
      letter-spacing: 0.03em;
      padding-left: 1.75rem;
      color: #262640;
    }
    &-list {
      overflow-y: auto;
      height: 100%;
      padding: 1.1rem 0.8rem 2rem 1.75rem;
      &::-webkit-scrollbar {
        width: 0.3rem;
        background: transparent;
        &-thumb {
          background: #5e37b0;
          box-shadow: 0 0.5rem 5rem rgba(94, 55, 176, 0.6);
          border-radius: 0.2rem;
        }
      }
      &-item {
        display: flex;
        align-items: center;
        margin-bottom: 1.05rem;
        &:last-child {
          margin-bottom: 0;
        }
        &__icon {
          width: 2.1rem;
          height: 2.1rem;
          background: #ffffff;
          box-shadow: 0 0.5rem 2rem rgba(24, 33, 65, 0.31);
          border-radius: 0.4rem;
          display: flex;
          align-items: center;
          justify-content: center;
          margin-right: 0.8rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          background-image: url("/img/bank/negative.png");
          &.positive {
            background-image: url("/img/bank/positive.png");
          }
        }
        &__info {
          display: flex;
          flex-direction: column;
          font-size: 1rem;
          &-value {
            font-weight: bold;
            letter-spacing: 0.03em;
            color: #030c20;
            text-shadow: 0 0.5rem 5rem rgba(94, 55, 176, 0.6);
            margin-bottom: 0.2rem;
          }
          &-desc {
            letter-spacing: 0.05em;
            font-weight: normal;
            color: rgba(3, 12, 32, 0.4);
          }
        }
      }
    }
  }
  &__services {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 3.95rem;
    &-item {
      width: 11rem;
      height: 11rem;
      background: radial-gradient(
        84.55% 84.55% at -21.36% -21.14%,
        #5e37b0 0%,
        rgba(94, 55, 176, 0) 100%
      );
      background-color: #fff;
      border-radius: 1rem;
      display: flex;
      justify-content: center;
      padding: 6rem 1.5rem 0 1.5rem;
      position: relative;
      &:before {
        content: "";
        width: 5rem;
        height: 5rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        position: absolute;
        top: 0.5rem;
      }
      &.balance {
        &:before {
          background-image: url("/img/bank/balance.png");
        }
      }
      &.money {
        &:before {
          background-image: url("/img/bank/money.png");
        }
      }
      &.transfer {
        &:before {
          background-image: url("/img/bank/transfer.png");
        }
      }
      &-text {
        font-size: 1.5rem;
        line-height: 1.5rem;
        text-align: center;
        letter-spacing: 0.03em;
        color: #262640;
      }
      .bank-main__btn {
        position: absolute;
        bottom: 0;
        transform: translateY(50%);
        &:hover {
          transform: translateY(50%) scale(1.1);
        }
        &:active {
          transform: translateY(50%) scale(1.05);
        }
      }
    }
  }
  &__btn {
    background: #ffffff;
    box-shadow: 0 1rem 4.5rem #0a041e;
    border-radius: 2.5rem;
    width: 5rem;
    min-width: 5rem;
    height: 2.5rem;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: 0.2s;
    &:before {
      content: "";
      width: 1.5rem;
      height: 0.75rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url("/img/bank/arrow.png");
    }
  }
  &__prompt {
    width: 100%;
    position: relative;
    font-weight: normal;
    font-size: 1.2rem;
    line-height: 1.45rem;
    letter-spacing: 0.03em;
    display: flex;
    align-items: center;
    margin-bottom: 0.85rem;
    &:after {
      content: "";
      width: 100%;
      height: 0.2rem;
      background: rgba(255, 255, 255, 0.14);
      margin-left: 1.85rem;
    }
  }
  &__info {
    display: grid;
    grid-auto-rows: 8rem;
    grid-template-columns: repeat(2, 1fr);
    grid-gap: 1rem;
    &-item {
      display: flex;
      flex-direction: column;
      position: relative;
      background: rgba(3, 12, 32, 0.7);
      border-radius: 1rem;
      padding: 2rem 1.5rem 0 1.5rem;
      transition: 0.1s;
      &:last-child {
        grid-column-start: 1;
        grid-column-end: 3;
      }
      &.hovered {
        &:hover {
          transform: scale(1.06);
        }
        &:active {
          transform: scale(1.03);
        }
      }
      &-title {
        font-weight: normal;
        font-size: 1.7rem;
        line-height: 2.05rem;
        letter-spacing: 0.03em;
        margin-bottom: 0.55rem;
      }
      &-desc {
        font-weight: normal;
        font-size: 1rem;
        line-height: 1.2rem;
        letter-spacing: 0.03em;
        color: rgba(255, 255, 255, 0.4);
        max-width: 15rem;
      }
      &-icon {
        position: absolute;
        top: 1rem;
        right: 1rem;
        width: 3rem;
        height: 3rem;
        background-repeat: no-repeat;
        background-position: center;
        background-size: contain;
        border-radius: 50%;
        &.house {
          background-image: url("/img/bank/circle-house.png");
        }
        &.biz {
          background-image: url("/img/bank/circle-biz.png");
        }
        &.fine {
          background-image: url("/img/bank/circle-fine.png");
        }
        &.mobile {
          background-image: url("/img/bank/circle-mobile.png");
        }
      }
      &__input {
        display: flex;
        align-items: center;
        position: absolute;
        width: 16.5rem;
        right: 2.5rem;
        padding: 0.25rem;
        background: rgba(255, 255, 255, 0.1);
        border: 1px solid rgba(255, 255, 255, 0.3);
        box-sizing: border-box;
        border-radius: 3rem;
        input {
          width: 100%;
          padding: 0 0 0 1.5rem;
          background: transparent;
          height: 100%;
          font-weight: normal;
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          color: #fff;
          &::placeholder {
            color: rgba(255, 255, 255, 0.5);
          }
          &::-webkit-outer-spin-button,
          &::-webkit-inner-spin-button {
            -webkit-appearance: none;
          }
        }
      }
    }
  }
} */
</style>
