<template>
  <div class="bank-org">
    <div class="bank-org_info">
      <div class="bank-org_header">
        <div
          class="bank-org_header-item"
          v-for="(elem, index) in [
            'The name of the organization',
            'Balance of the organization',
          ]"
          :key="index"
        >
          <div>{{ elem }}</div>
          <div>
            {{
              index === 0
                ? organizationName
                : `$${organizationBalance.toLocaleString("ru")}`
            }}
          </div>
        </div>
      </div>
      <div class="bank-org_btns">
        <button
          class="bank-main_services-item"
          v-for="(elem, index) in [
            ['Das Gleichgewicht auffüllen ', 'TopUp', 'topup'],
            ['Mittel entfernen', 'Withdraw', 'widthdraw'],
            ['auf ein anderes Konto übertragen', 'Transfer', 'transfer'],
            ['Boni zahlen', 'Bonus', 'transfer'],
          ]"
          :key="index"
          @click="() => setModal(elem[1])"
        >
          <img :src="`/img/bank/main/${elem[2]}.svg`" alt="" />
          <div class="bank-main_services-text">{{ elem[0] }}</div>
        </button>
      </div>

      <div class="bank-biz_income">
        <img src="/img/bank/business/info.png" alt="" />
        <div class="bank-biz_income-info">
          <div>INCOME</div>
          <div>+&nbsp;${{ calcSum(profitArray).toLocaleString("ru") }}</div>
        </div>
      </div>
    </div>

    <transition name="bank-modal">
      <component
        :fractionList="fractionList"
        v-if="currentModal"
        :is="currentModal"
        @closeModal="closeModal"
      />
    </transition>

    <!-- <div class="bank-org_info">
      <div class="bank-org_header">
        <div
          class="bank-org_header-item"
          v-for="(elem, index) in ['Название семьи', 'БАланс семьи']"
          :key="index"
        >
          <div>{{ elem }}</div>
          <div>
            {{
              index === 0
                ? organizationName
                : `$${organizationBalance.toLocaleString("ru")}`
            }}
          </div>
        </div>
      </div>
      <div class="bank-org_btns">
        <button
          class="bank-main_services-item"
          v-for="(elem, index) in [
            ['Fill the balance', 'TopUp', 'topup'],
            ['Gelder entfernen', 'Withdraw', 'widthdraw'],
            ['transfer to another account', 'Transfer', 'transfer'],
            ['transfer to another account', 'Bonus', 'transfer'],
          ]"
          :key="index"
          @click="() => setModal(elem[1])"
        >
          <img :src="`/img/bank/main/${elem[2]}.svg`" alt="" />
          <div class="bank-main_services-text">{{ elem[0] }}</div>
        </button>
      </div>

      <div class="bank-org_income">
        <div class="bank-org_income-diagram">
          <div>
            <div>Work</div>
            <div>+&nbsp;${{ 100 }}</div>
          </div>
          <div>
            <svg viewBox="0 0 140 140" xmlns="http://www.w3.org/2000/svg">
              <circle
                cx="70"
                cy="70"
                r="58"
                stroke="#FFCA42"
                stroke-width="22"
                fill="none"
                :stroke-dasharray="2 * Math.PI * 58"
                :stroke-dashoffset="0"
              />
              <circle
                cx="70"
                cy="70"
                r="58"
                stroke="#78FFBE"
                stroke-width="22"
                fill="none"
                :stroke-dashoffset="
                  2 * Math.PI * 58 - ((2 * Math.PI * 58) / 100) * 50
                "
                :stroke-dasharray="2 * Math.PI * 58"
              />
            </svg>
          </div>
          <div>
            <div>общее</div>
            <div>+&nbsp;${{ 100 }}</div>
          </div>
        </div>
        <div class="bank-org_income-info">
          <div>ДОХОД Семьи</div>
          <div>+&nbsp;${{ calcSum(profitArray).toLocaleString("ru") }}</div>
        </div>
      </div>
    </div>
 -->
    <div class="bank-org_transfer">
      <div class="bank-main_title">Your transactions</div>

      <div class="bank-main_transaction-wrap">
        <div
          class="bank-main_transaction-item"
          v-for="(item, index) in getTransfers"
          :key="index"
        >
          <div :class="item.directFrom ? 'low' : 'high'">
            <img
              :src="`/img/bank/icon/${item.directFrom ? 'low' : 'high'}.png`"
              alt=""
            />
          </div>
          <div>{{ loc(item.comment) }}</div>
          <div>{{item.directFrom ? '-' : '+'}}&nbsp;${{ getAmount(item) }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Transfer from "./modals/Transfer";
import { mapState, mapGetters } from "vuex";

export default {
  name: "BankOrganization",
  data() {
    return {
      currentModal: null,
    };
  },
  components: { Transfer },
  computed: {
    ...mapState("bank/organization", [
      "organizationName",
      "transfersList",
      "organizationBalance",
      "staffList",
      "fractionList",
    ]),
    ...mapGetters("localization", ["loc"]),
    getTransfers() {
      return this.transfersList.slice().sort((a, b) => b.date - a.date);
    },
    profitArray() {
      return this.transfersList.filter((element) => !element.directFrom);
    },
  },
  methods: {
    setCurrentModal(value) {
      this.currentModal = value;
    },
    closeModal() {
      this.currentModal = null;
    },
    setModal(name) {
      if (name === "Bonus") {
        this.$emit("setCurrentModal", {
          component: "Bonus",
          dataModal: {
            type: "fraction",
            staffList: this.staffList,
          },
        });
      } else if (name === "Transfer") {
        this.setCurrentModal(name);
      } else {
        this.$emit("setCurrentModal", {
          component: name,
          dataModal: {
            type: "fraction",
            balance: this.organizationBalance,
          },
        });
      }
    },
    calcSum(arr) {
      let sum = 0;
      arr.forEach(function (item) {
        sum += item.value - item.tax;
      });
      return sum;
    },
    getIcon(transact) {
      if (!transact.directFrom) return "replenishment-icon";
      else return "withdrawal-icon";
      // bonus{
      //   background-image: url('/img/bank/bonus-icon.png');
      // }
      // withdrawal{
      //   background-image: url('/img/bank/withdrawal-icon.png');
      // }
      // tax{
      //   background-image: url('/img/bank/suitcase.png');
      // }
    },
    getAmount(transact) {
      if (transact.directFrom) return transact.value.toLocaleString("ru");
      else return (transact.value - transact.tax).toLocaleString("ru");
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

.bank-org {
  padding: conv(14, true) conv(260) conv(69, true);
  display: grid;
  grid-template-columns: conv(717) conv(633);
  column-gap: conv(50);

  & > div:not(.bank-modal) {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;

    background: linear-gradient(
      359.35deg,
      rgba(255, 255, 255, 0.04) 0.55%,
      rgba(255, 255, 255, 0) 99.44%
    );
  }

  &_info {
    display: flex;
    justify-content: space-between;
    flex-direction: column;
    padding: conv(72, true) 0 0 conv(40);/* conv(22, true) */
    position: relative;
    overflow: hidden;
  }

  &_header {
    &-item {
      &:last-child {
        margin-top: conv(29, true);
      }

      div {
        font-weight: 700;
        text-transform: uppercase;

        &:first-child {
          font-size: conv(20, true);
          line-height: conv(24, true);
          color: rgba(255, 255, 255, 0.55);
        }

        &:last-child {
          font-size: conv(40, true);
          line-height: conv(48, true);
          color: #ffffff;
        }
      }
    }
  }

  &_btns {
    position: absolute;
    display: flex;
    flex-direction: column;
    width: conv(300);
    height: auto;
    bottom: conv(297, true);
    right: conv(93);
  }

  &_income {
    display: flex;
    flex-direction: column;
    align-items: center;

    &-diagram {
      display: flex;
      align-items: center;

      & > div {
        &:first-child,
        &:last-child {
          display: flex;
          align-items: center;
          flex-direction: column;
          width: conv(142);

          div {
            font-weight: 700;
            text-align: center;
            text-transform: uppercase;

            &:first-child {
              margin-bottom: conv(9, true);
              display: flex;
              justify-content: center;
              align-items: center;
              width: 100%;
              height: conv(51, true);
              border-radius: conv(34);
              font-size: conv(20, true);
              line-height: conv(24, true);
              color: #242826;
            }

            &:last-child {
              font-size: conv(24, true);
              line-height: conv(29, true);
              color: #ffffff;
            }
          }
        }

        &:first-child {
          margin-right: conv(38);

          div:first-child {
            background: #78ffbe;
          }
        }

        &:nth-child(2) {
          width: conv(140, true);
          height: conv(140, true);
        }

        &:last-child {
          margin-left: conv(38);

          div:first-child {
            background: #ffca42;
          }
        }
      }
    }

    &-info {
      margin-top: conv(25, true);
      display: flex;
      align-items: center;
      flex-direction: column;

      div {
        font-weight: 700;
        text-transform: uppercase;
        text-align: center;

        &:first-child {
          font-size: conv(16, true);
          line-height: conv(19, true);
          color: rgba(255, 255, 255, 0.55);
        }

        &:last-child {
          margin-top: conv(1, true);
          font-size: conv(36, true);
          line-height: conv(43, true);
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }
  }

  &_transfer {
    padding: conv(64, true) 0 0 0;
  }

  .bank-main_transaction-item {
    display: grid;
    grid-template-columns: conv(76, true) 12fr 4fr;

    & > div:last-child {
      font-size: conv(20, true);
      line-height: conv(24, true);
      color: #ffffff;
    }
  }
}

/* .bank-organization{
  width: 64rem;
  height: 43rem;
  display: flex;
  justify-content: space-between;
  &:before{
    content: '';
    width: 14.65rem;
    height: 48.15rem;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: bottom right;
    position: absolute;
    right: 0;
    bottom: 0;
    background-image: url('/img/bank/decorate-organization.png');
    pointer-events: none;
    // animation: showOrganization 1s;
  }
  &__btn{
    width: 100%;
    height: 4rem;
    display: flex;
    align-items: center;
    justify-content: center;
    border: .1rem solid rgba(255, 255, 255, 0.4);
    background-color: transparent;
    border-radius: .2rem;
    margin-bottom: 2rem;
    transition: .2s;
    &:hover{
      background-color: rgba(255, 255, 255, 0.1);
    }
  }
  &__prompt{
    font-weight: normal;
    font-size: 1rem;
    line-height: 1.2rem;
    letter-spacing: 0.03em;
    color: rgba(255, 255, 255, 0.5);
    margin-bottom: .3rem;
  }
  &__info{
    display: flex;
    flex-direction: column;
    width: 20rem;
    height: 100%;
    justify-content: space-between;
    &-desc{
      font-weight: normal;
      font-size: 1.2rem;
      line-height: 1.45rem;
      letter-spacing: 0.03em;
      color: #B6D300;
      margin-bottom: .5rem;
    }
    &-title{
      font-weight: bold;
      font-size: 3.5rem;
      line-height: 3.5rem;
      letter-spacing: 0.03em;
      min-height: 7rem;
      margin-bottom: 1.5rem;
    }
    &-balance{
      font-weight: bold;
      font-size: 2.5rem;
      line-height: 2.5rem;
      letter-spacing: 0.03em;
      margin-bottom: 1.5rem;
    }
  }
  &__income{
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    background-image: url('/img/bank/profit-bg.png');
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center bottom;
    border-radius: 0 0 .5rem .5rem;
    border-top: 1px solid rgba(255, 255, 255, 0.2);
    padding: 2.25rem 2rem 3rem 2rem;
    height: 21.5rem;
    position: relative;
    &-display{
      position: absolute;
      top: 3rem;
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      .bank-organization__prompt{
        color: #fff;
      }
    }
    &-desc{
      font-weight: normal;
      font-size: 1.1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
    }
    &-value{
      font-weight: normal;
      font-size: 2rem;
      line-height: 2.4rem;
      color: #B6D300;
    }
    &-prompt{
      width: 100%;
      display: flex;
      align-items: center;
      justify-content: space-between;
      padding-top: 1rem;
      border-top: 1px solid rgba(255, 255, 255, 0.2);
      &-item{
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        line-height: 1.2rem;
        letter-spacing: 0.03em;
        &-desc{
          width: 7.5rem;
          height: 2.5rem;
          display: flex;
          align-items: center;
          justify-content: center;
          font-size: 1rem;
          margin-bottom: .5rem;
          border-radius: 5rem;
          background: rgba(94, 92, 230, 0.2);
          color: #5E5CE6;
          &.job{
            background: rgba(220, 186, 88, 0.2);
            color: #DCBA58;
          }
        }
        &-value{
          font-size: 1.1rem;
          color: rgba(255, 255, 255, 0.4);
        }
      }
    }
  }
  &__transfers{
    position: relative;
    padding: 2.5rem 3.5rem 3.5rem 3.5rem;
    background: rgba(3, 12, 32, 0.7);
    border-radius: 1rem;
    width: 37.4rem;
    height: 100%;
    &-title{
      font-weight: bold;
      font-size: 2.5rem;
      line-height: 2.5rem;
      letter-spacing: 0.03em;
    }
    &-list{
      margin-top: 1.3rem;
      overflow-y: auto;
      height: 30.6rem;
      margin-left: 2.5rem;
      padding-right: 1.5rem;
      margin-right: -1.5rem;
      &::-webkit-scrollbar{
        width: .2rem;
        background-color: transparent;
        &-thumb{
          background: rgba(255, 255, 255, 0.2);
          border-radius: .3rem;
        }
      }
      &-item{
        display: flex;
        align-items: center;
        padding-bottom: .6rem;
        border-bottom: 1px solid rgba(255, 255, 255, 0.2);
        margin-bottom: 1rem;
        &:last-child{
          margin-bottom: 0;
          border-bottom: 0;
        }
        &__icon{
          width: 2.3rem;
          min-width: 2.3rem;
          height: 2.3rem;
          background-color: rgba(255, 255, 255, 0.14);
          border-radius: .3rem;
          display: flex;
          align-items: center;
          justify-content: center;
          margin-right: .8rem;
          span{
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
            width: 1.2rem;
            height: 1.2rem;
            &.replenishment{
              background-image: url('/img/bank/replenishment-icon.png');
            }
            &.bonus{
              background-image: url('/img/bank/bonus-icon.png');
            }
            &.withdrawal{
              background-image: url('/img/bank/withdrawal-icon.png');
            }
            &.transfer{
              background-image: url('/img/bank/transfer-icon.png');
            }
          }
        }
        &__info{
          display: flex;
          flex-direction: column;
          white-space: pre;
          &-name{
            font-weight: bold;
            font-size: 1rem;
            line-height: 1rem;
            letter-spacing: 0.03em;
            text-shadow: 0 .5rem 5rem rgba(94, 55, 176, 0.6);
          }
          &-desc{
            font-weight: normal;
            font-size: 1rem;
            line-height: 1.2rem;
            letter-spacing: 0.05em;
            color: rgba(255, 255, 255, 0.4);
          }
        }
        &-value{
          width: 100%;
          text-align: right;
          font-weight: bold;
          font-size: 1.2rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          &:before{
            content: '- ';
          }
          &.profit{
            color: #B6D300;
            text-shadow: 0 .5rem 5rem rgba(94, 55, 176, 0.6);
            &:before{
              content: '+ ';
            }
          }
        }
      }
    }
    &__btns{
      position: absolute;
      left: 0;
      top: 50%;
      transform: translateX(-50%) translateY(-50%);
    }
    &__btn{
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      width: 8rem;
      height: 8rem;
      margin-bottom: .5rem;
      background: #5E37B0;
      border-radius: .2rem;
      position: relative;
      padding: 0 2rem;
      font-weight: normal;
      text-align: center;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      transition: .2s;
      &:hover{
        background-color: #452883;
      }
      &:before{
        content: '';
        width: 2rem;
        height: 2rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        margin-bottom: .7rem;
      }
      &.replenishment{
        &:before {
          background-image: url('/img/bank/replenishment-icon.png');
        }
      }
      &.bonus{
        &:before {
          background-image: url('/img/bank/bonus-icon.png');
        }
      }
      &.withdrawal{
        &:before {
          background-image: url('/img/bank/withdrawal-icon.png');
        }
      }
    }
  }
  @keyframes showOrganization{
    from {
      transform: translateX(100%);
    }
    to {
      transform: translateX(0);
    }
  }
} */
</style>
