<template>
  <div class="bank-org bank-biz">
    <div class="bank-org_info">
      <div class="bank-org_header">
        <div
          class="bank-org_header-item"
          v-for="(elem, index) in ['Business name', 'Account balance']"
          :key="index"
        >
          <div>{{ elem }}</div>
          <div>
            {{ index === 0 ? bizName : `$${bizBalance.toLocaleString("ru")}` }}
          </div>
        </div>
      </div>
      <div class="bank-org_btns">
        <button
          class="bank-main_services-item"
          v-for="(elem, index) in [
            ['das Guthaben auffüllen ', 'TopUp', 'topup'],
            ['Gelder entfernen', 'Withdraw', 'widthdraw'],
            ['Zahlung des Geschäfts', 'PayForBiz', 'transfer'],
          ]"
          :key="index"
          @click="() => setModals(elem[1])"
        >
          <img :src="`/img/bank/main/${elem[2]}.svg`" alt="" />
          <div class="bank-main_services-text">{{ elem[0] }}</div>
        </button>
      </div>

      <div class="bank-biz_income">
        <div class="perAllTime">
          <svg xmlns="http://www.w3.org/2000/svg" width="358" height="222" fill="none" viewBox="0 0 358 222" preserveAspectRatio="none">
            <path fill="url(#a)" fill-opacity=".35" d="M326.48 53.669 266.032 14l-53.565 48-89.177-31.5-52.667 23.169L32.917 0 0 44.813V255h358V28l-31.52 25.669Z"/>
            <defs>
              <linearGradient id="a" x1="179.25" x2="179.25" y1="-16" y2="222" gradientUnits="userSpaceOnUse">
                <stop :stop-color="isPositiveValue(getProfitPerAllTime) ? '#8AFF61' : '#FF6161'"/>
                <stop offset="1" :stop-color="isPositiveValue(getProfitPerAllTime) ? '#8AFF61' : '#FF6161'" stop-opacity="0"/>
              </linearGradient>
            </defs>
          </svg>
          <div class="info">
            <div class="title">Profitability</div>
            <div class="subtitle">For the whole time</div>
            <div class="value">
             <span>{{isPositiveValue(getProfitPerAllTime) ? '+' : '-'}}</span>${{ Math.abs(getProfitPerAllTime).toLocaleString("ru") }}
            </div>
          </div>
        </div>
        <div class="perMonth">
          <svg xmlns="http://www.w3.org/2000/svg" width="359" height="222" fill="none" viewBox="0 0 359 222" preserveAspectRatio="none">
            <path fill="url(#b)" fill-opacity=".35" d="M326.48 53.669 266.032 14l-53.565 48-89.176-31.5-52.668 23.169L32.917 0 0 28v227h358.5V37.137L326.48 53.67Z"/>
            <defs>
              <linearGradient id="b" x1="179.25" x2="179.25" y1="-16" y2="222" gradientUnits="userSpaceOnUse">
                <stop :stop-color="isPositiveValue(getProfitPerMonth) ? '#8AFF61' : '#FF6161'"/>
                <stop offset="1" :stop-color="isPositiveValue(getProfitPerMonth) ? '#8AFF61' : '#FF6161'" stop-opacity="0"/>
              </linearGradient>
            </defs>
          </svg>
          <div class="info">
            <div class="title">Profitability</div>
            <div class="subtitle">For a month</div>
            <div class="value">
              <span>{{isPositiveValue(getProfitPerMonth) ? '+' : '-'}}</span> ${{ Math.abs(getProfitPerMonth).toLocaleString("ru") }}
            </div>
          </div>
        </div>
        <svg class="layer" xmlns="http://www.w3.org/2000/svg" width="717" height="126" fill="none" viewBox="0 0 717 126" preserveAspectRatio="none">
          <path fill="url(#c)" fill-opacity=".05" d="M219.244 42.393 90.681 10.727 0 21v105h717V0L543.369 31.81 391.87 20.944l-172.626 21.45Z"/>
          <defs>
            <linearGradient id="c" x1="358.5" x2="358.5" y1="0" y2="126" gradientUnits="userSpaceOnUse">
              <stop stop-color="#D9D9D9"/>
              <stop offset="1" stop-color="#D9D9D9" stop-opacity="0"/>
            </linearGradient>
          </defs>
        </svg>
        <!-- <img src="/img/bank/business/info.png" alt="" />
        <div class="bank-biz_income-info">
          <div>ДОХОД</div>
          <div>+&nbsp;${{ getProfit.toLocaleString("ru") }}</div>
        </div> -->
      </div>
    </div>

    <!-- <div class="bank-business__interaction">
      <div class="bank-business__interaction--wrap">
        <div class="bank-business__interaction-desc">
          {{ loc("bank:menu:206") }}
        </div>
        <div class="bank-business__interaction-title">{{ bizName }}</div>
        <div class="bank-business__interaction-row">
          <div class="bank-business__interaction-row__col">
            <div class="bank-business__prompt">{{ loc("bank:menu:207") }}</div>
            <div class="bank-business__interaction-balance">
              $ {{ bizBalance.toLocaleString("ru") }}
            </div>
          </div>
          <div
            :class="[{ active: isCredit }, 'bank-business__interaction-btn']"
            @click="bizCredit"
          >
            {{ loc("bank:menu:208") }}
          </div>
        </div>
        <div class="bank-business__interaction-profit">
          <div class="bank-business__prompt">{{ loc("bank:menu:209") }}</div>
          <div class="bank-business__interaction-profit-value">
            $ {{ bizProfit }}
          </div>
        </div>
      </div>
      <div class="bank-business__interaction-btns">
        <div
          :class="[
            { locked: !accessList.topUp },
            'bank-business__interaction-btns-item',
          ]"
          @click="
            $emit('setCurrentModal', {
              component: 'TopUp',
              dataModal: {
                type: 'business',
                balance: bizBalance,
              },
            })
          "
        >
          <div class="icon replenishment"></div>
          <span>{{ loc("bank:menu:210") }}</span>
        </div>
        <div
          :class="[
            { locked: !accessList.withdraw },
            'bank-business__interaction-btns-item',
          ]"
          @click="
            $emit('setCurrentModal', {
              component: 'Withdraw',
              dataModal: {
                type: 'business',
                balance: bizBalance,
              },
            })
          "
        >
          <div class="icon withdrawal"></div>
          <span>{{ loc("bank:menu:211") }}</span>
        </div>
        <div
          :class="[
            { locked: !accessList.bonus },
            'bank-business__interaction-btns-item',
          ]"
          @click="
            $emit('setCurrentModal', {
              component: 'Bonus',
              dataModal: {
                type: 'business',
                staffList: staffList,
              },
            })
          "
        >
          <div class="icon bonus"></div>
          <span>{{ loc("bank:menu:212") }}</span>
        </div>
        <div
          :class="[
            { locked: !accessList.payForBiz },
            'bank-business__interaction-btns-item',
          ]"
          @click="
            $emit('setCurrentModal', {
              component: 'PayForBiz',
              dataModal: {
                type: 'business',
              },
            })
          "
        >
          <div class="icon tax"></div>
          <span>{{ loc("bank:menu:213") }}</span>
        </div>
      </div>
    </div> -->
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
          <div>{{item.directFrom ? '-' : '+'}} &nbsp;${{ getAmount(item) }}</div>
        </div>
      </div>
    </div>
    <transition name="bank-modal">
      <component
        :is="currentModal"
        v-if="currentModal"
        @closeModal="closeModal"
      />
    </transition>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
import Credit from "./modals/BankBusinessCredit";
import CurrentCredit from "./modals/BankBusinessCurrentCredit";
export default {
  name: "Bankbusiness",
  components: {
    Credit,
    CurrentCredit,
  },
  data() {
    return {
      currentModal: null,
    };
  },
  computed: {
    ...mapState("bank/business", [
      "bizName",
      "transfersList",
      "bizBalance",
      "bizProfit",
      "isCredit",
      "accessList",
      "staffList",
    ]),
    ...mapGetters("localization", ["loc"]),
    getProfitPerMonth() {
      return this.bizProfit.Income - this.bizProfit.Expenses;
    },
    getProfitPerAllTime() {
      return this.bizProfit.TotalIncome - this.bizProfit.TotalExpenses;
    },
    getTransfers() {
      return this.transfersList.slice().sort((a, b) => b.date - a.date);
    },
  },
  methods: {
    bizCredit() {
      this.$emit("setCurrentPage", "Credit");
    },
    setModal(value) {
      this.currentModal = value;
    },
    isPositiveValue(v){
      return v >= 0
    },
    setModals(elem) {
      if (elem === "Credit") {
        // nothing
      } else if (elem === "PayForBiz") {
        this.$emit("setCurrentModal", {
          component: "PayForBiz",
          dataModal: {
            type: "business",
          },
        });
      } else {
        this.$emit("setCurrentModal", {
          component: elem,
          dataModal: {
            type: "business",
            balance: this.bizBalance,
          },
        });
      }
    },
    closeModal() {
      this.currentModal = null;
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

.bank-biz {
  display: grid;
  grid-template-columns: conv(717) conv(633);
  column-gap: conv(50);

  .bank-org_info {
    padding-bottom: 0;
    overflow: hidden;
  }

  &_income {
    display: flex;
    align-items: center;
    position: absolute;
    left: 0;
    bottom: 0;
    width: 100%;
    .layer {
      position: absolute;
      bottom: 0;
      left: 0;
    }
    & > div {
      position: relative;
      width: 50%;
      overflow: hidden;
      svg {
        width: 100%;
      }
      .info {
        position: absolute;
        z-index: 2;
        top: 55%;
        left: 50%;
        transform: translate(-50%, -50%);
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 0.37vh;
        font-weight: 700;
        color: #fff;
        .value {
          color: #5CFF80;
          margin-top: 0.926vh;
          font-size: 4.444vh;
          line-height: 5.37vh;
          position: relative;
          font-weight: 700;
          span {
            position: absolute;
            right: 100%;
            transform: translateX(-0.926vh);
          }
        }
      }
    }
    // img {
    //   position: absolute;
    //   width: calc(100% + 2.0833333333vw);
    //   height: conv(255, true);
    //   bottom: 0;
    //   left: 50%;
    //   transform: translateX(-50%);
    // }

    // &-info {
    //   padding-bottom: conv(53, true);

    //   div {
    //     font-weight: 700;
    //     text-align: center;
    //     text-transform: uppercase;
    //   }

    //   div:first-child {
    //     font-size: conv(20, true);
    //     line-height: conv(24, true);
    //     color: #ffffff;
    //     margin-bottom: conv(5, true);
    //   }

    //   div:last-child {
    //     font-size: conv(48, true);
    //     line-height: conv(58, true);
    //     color: #5cff80;
    //   }
    // }
  }
}

/* .bank-business{
  width: 64rem;
  height: 43rem;
  display: flex;
  justify-content: space-between;
  &:before{
    content: '';
    width: 13.8rem;
    height: 54rem;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: bottom right;
    position: absolute;
    right: 0;
    bottom: 0;
    background-image: url('/img/bank/decorate-business.png');
    pointer-events: none;
    // animation: showbusiness 1s;
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
  &__interaction{
    display: flex;
    flex-direction: column;
    width: 29rem;
    height: 100%;
    justify-content: space-between;
    &--wrap{
      display: flex;
      flex-direction: column;
      padding: 2.5rem 2.5rem 1rem 2.5rem;
      background: rgba(3, 12, 32, 0.7);
      border-radius: 1rem;
      height: 22.5rem;
      margin-bottom: 1.5rem;
    }
    &-desc{
      font-weight: normal;
      font-size: 1.2rem;
      line-height: 1.45rem;
      letter-spacing: 0.03em;
      color: #B6D300;
    }
    &-title{
      font-weight: bold;
      font-size: 2.4rem;
      line-height: 2.4rem;
      letter-spacing: 0.03em;
      min-height: 4rem;
    }
    &-balance{
      font-weight: normal;
      font-size: 1.8rem;
      line-height: 2.15rem;
      letter-spacing: 0.03em;
    }
    &-row{
      display: flex;
      align-items: center;
      justify-content: space-between;
      margin-bottom: 1.5rem;
      &__col{
        display: flex;
        flex-direction: column;
      }
    }
    &-btn{
      width: 10rem;
      height: 3rem;
      display: flex;
      align-items: center;
      justify-content: center;
      border: .1rem solid rgba(255, 255, 255, 0.4);
      box-sizing: border-box;
      border-radius: .2rem;
      font-weight: normal;
      font-size: 1rem;
      letter-spacing: 0.03em;
      color: rgba(255, 255, 255, 0.4);
      transition: .2s;
      &:not(.active):hover{
        border-color: rgba(255, 255, 255, 1);
        color: rgba(255, 255, 255, 1);
      }
      &.active{
        border-color: #EE443A;
        color: #EE443A;
        &:hover{
          color: #FFFFFF;
          background-color: #EE443A;
        }
      }
    }
    &-profit{
      height: 100%;
      width: calc(100% + (3rem));
      margin-left: -1.5rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url('/img/bank/biz-info-bg.png');
      display: flex;
      align-items: center;
      justify-content: center;
      flex-direction: column;
      position: relative;
      &-value{
        font-weight: bold;
        font-size: 2rem;
      }
      &:before{
        content: '';
        width: 6.55rem;
        height: 7.35rem;
        position: absolute;
        left: -2.55rem;
        bottom: -1.9rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        background-image: url('/img/bank/biz-info-decorate.png');
      }
    }
    &-btns{
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      grid-gap: 1rem;
      grid-auto-rows: 9rem;
      &-item{
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        background: rgba(3, 12, 32, 0.6);
        border: .1rem solid rgba(255, 255, 255, 0.2);
        box-sizing: border-box;
        border-radius: .8rem;
        position: relative;
        .icon{
          width: 2rem;
          height: 2rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          margin-bottom: .8rem;
          &.withdrawal{
            background-image: url('/img/bank/withdrawal-icon.png');
          }
          &.replenishment{
            background-image: url('/img/bank/replenishment-icon.png');
          }
          &.bonus{
            background-image: url('/img/bank/bonus-icon.png');
          }
          &.tax{
            background-image: url('/img/bank/suitcase.png');
          }
        }
        span{
          font-weight: normal;
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
        }
        &.locked{
          pointer-events: none;
          .icon, span{
            opacity: .3;
          }
          &:before{
            content: '';
            width: 2.2rem;
            height: 2.2rem;
            border-radius: 50%;
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
            background-color: #fff;
            background-image: url('/img/bank/locked.png');
            position: absolute;
            left: 1rem;
            top: 1rem;
          }
        }
      }
      &-item:not(.locked){
        &::before{
          content: '';
          width: 100%;
          height: 100%;
          background: radial-gradient(84.55% 84.55% at -21.36% -21.14%, #5E37B0 0%, rgba(94, 55, 176, 0) 100%);
          position: absolute;
          left: -.1rem;
          top: -.1rem;
          border-radius: .8rem;
          opacity: .7;
          transition: .3s;
        }
        &:hover{
          &::before{
            opacity: 1;
          }
        }
      }
    }
  }
  &__transfers{
    position: relative;
    padding: 3rem;
    background: rgba(3, 12, 32, 0.7);
    border-radius: 1rem;
    width: 33.5rem;
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
            &.tax{
              background-image: url('/img/bank/suitcase.png');
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
  }
  @keyframes showbusiness{
    from {
      transform: translateX(100%);
    }
    to {
      transform: translateX(0);
    }
  }
} */
</style>
