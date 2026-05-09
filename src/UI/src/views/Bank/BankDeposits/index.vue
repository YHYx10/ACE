<template>
  <div class="bank-deposits">
    <transition name="bank-modal">
      <BankDepositsModal
        v-if="modal.show"
        :dataModal="modal.dataModal"
        @closeModal="closeModal"
      />
    </transition>
    <div class="bank-deposits_current">
      <div class="bank-deposits_items" v-if="currentDepositsList.length > 0">
        <div class="bank-deposits_items-wrap">
          <div class="bank-deposits_items-wrap_img">
            <img src="/img/bank/deposits/bg.png" alt="" />
            <img src="/img/bank/deposits/bg.png" alt="" />
          </div>
          <div class="bank-deposits_items-wrap_content">
            <div class="bank-deposits_items-wrap_info">
              <div>
      Information about the post

                <img
                  :class="{ active: currentDeposit.IsRefill }"
                  src="/img/bank/deposits/refill.svg"
                  alt=""
                />
                <img
                  :class="{ active: currentDeposit.IsWithdraw }"
                  src="/img/bank/deposits/withdraw.svg"
                  alt=""
                />
              </div>
              <div>{{ loc(currentDeposit.Title) }}</div>
              <div>{{ loc(currentDeposit.Description) }}</div>
            </div>
            <div class="bank-deposits_items-wrap_bet">
              <div>Bet</div>
              <div>{{ currentDeposit.AnnualRate }}%</div>
            </div>
            <div class="bank-deposits_items-wrap_balance">
              <div>Balance in the account</div>
              <div>
                <div>
                  ${{
                    currentDepositsList[
                      currentDepositIndex
                    ].balance.toLocaleString("ru")
                  }}
                </div>
                <div class="price">
                  + ${{
                    currentDepositsList[
                      currentDepositIndex
                    ].profit.toLocaleString("ru")
                  }}
                </div>
              </div>
            </div>
            <div class="bank-deposits_items-wrap_balance">
              <div>It stays until the end</div>
              <div>{{ getLeftTime }}</div>
            </div>
          </div>
        </div>
        <div class="bank-deposits_items-bottom">
          <div class="bank-deposits_items-btns">
            <button
              class="refill"
              v-if="currentDeposit.IsRefill"
              @click="
                $emit('setCurrentModal', {
                  component: 'TopUp',
                  dataModal: {
                    type: `deposit`,
                    id: currentDepositsList[currentDepositIndex].id,
                    balance: currentDepositsList[currentDepositIndex].balance,
                  },
                })
              "
            >
              <img src="/img/bank/deposits/btn-refill.svg" alt="" />
          Fill out the remaining amount
            </button>
            <button
              class="withdraw"
              v-if="currentDeposit.IsWithdraw"
              @click="
                $emit('setCurrentModal', {
                  component: 'Withdraw',
                  dataModal: {
                    type: `deposit`,
                    id: currentDepositsList[currentDepositIndex].id,
                    balance: currentDepositsList[currentDepositIndex].balance,
                  },
                })
              "
            >
        Close the post
            </button>
          </div>
          <div
            class="bank-deposits_items-scroll"
            v-if="currentDepositsList.length > 1"
          >
            <div>Your contribution</div>
            <div>
              <button @click="prevDeposit">
                <img src="/img/bank/deposits/arrow.svg" alt="" />
              </button>
              <div>
                {{ currentDepositIndex + 1 }}/{{ currentDepositsList.length }}
              </div>
              <button @click="nextDeposit">
                <img src="/img/bank/deposits/arrow.svg" alt="" />
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="bank-deposits_empty" v-else>
        <div class="bank-deposits_empty-title">Deposits</div>
        <div class="bank-deposits_empty-descr">
Deposits - from a person or a legal person
to a financial institution or a company for storage, growth or for
Participation in receipt atby
        </div>
        <div class="bank-deposits_empty-red">You have no deposits!</div>
        <div class="bank-deposits_empty-subdescr">Select one of the posts</div>
      </div>
    </div>

    <!-- <div class="bank-deposits__current" v-if="currentDepositsList.length > 0">
      <div class="bank-deposits__current-prompt">
        {{
          loc(
            `bank:menu:111@${currentDepositIndex + 1}@${
              currentDepositsList.length
            }`
          )
        }}
      </div>
      <div class="bank-deposits__current-title">
        {{ loc(currentDeposit.Title) }}
      </div>
      <div class="bank-deposits__current-desc">
        {{ loc(currentDeposit.Description) }}
      </div>
      <div class="bank-deposits__current__circs">
        <div
          :class="[
            { active: currentDeposit.IsRefill },
            'bank-deposits__current__circs-item',
            'replenishment',
          ]"
        >
          <div class="bank-prompt">
            {{
              currentDeposit.IsRefill
                ? loc("bank:menu:112")
                : loc("bank:menu:113")
            }}
          </div>
        </div>
        <div
          :class="[
            { active: currentDeposit.IsWithdraw },
            'bank-deposits__current__circs-item',
            'completion',
          ]"
        >
          <div class="bank-prompt">
            {{
              currentDeposit.IsWithdraw
                ? loc("bank:menu:114")
                : loc("bank:menu:115")
            }}
          </div>
        </div>
        <div class="bank-deposits__current__circs__info">
          <div class="bank-deposits__current__circs__info-value">
            {{ currentDeposit.AnnualRate }}%
          </div>
          <div class="bank-deposits__current__circs__info-desc">
            {{ loc("bank:menu:116") }}
          </div>
        </div>
        <template v-if="currentDepositsList.length > 1">
          <div
            class="bank-deposits__current__btn prev"
            @click="prevDeposit"
          ></div>
          <div
            class="bank-deposits__current__btn next"
            @click="nextDeposit"
          ></div>
        </template>
      </div>
      <div class="bank-deposits__current__info">
        <div class="bank-deposits__current__info-desc">
          {{ loc("bank:menu:117") }}
        </div>
        <div class="bank-deposits__current__info-balance">
          {{
            currentDepositsList[currentDepositIndex].balance.toLocaleString(
              "ru"
            )
          }}
          $
        </div>
        <div class="bank-deposits__current__info-profit">
          +
          {{
            currentDepositsList[currentDepositIndex].profit.toLocaleString("ru")
          }}
          $
        </div>
        <div class="bank-deposits__current__info-desc">
          {{ loc("bank:menu:118") }}
        </div>
        <div class="bank-deposits__current__info-time">{{ getLeftTime }}</div>
      </div>
      <div
        class="bank-deposits__current-btn btn-main"
        v-if="currentDeposit.IsRefill"
        @click="
          $emit('setCurrentModal', {
            component: 'TopUp',
            dataModal: {
              type: `deposit`,
              id: currentDepositsList[currentDepositIndex].id,
              balance: currentDepositsList[currentDepositIndex].balance,
            },
          })
        "
      >
        {{ loc("bank:menu:119") }}
      </div>
      <div
        class="bank-deposits__current-btn btn-main withdraw"
        v-if="currentDeposit.IsWithdraw"
        @click="
          $emit('setCurrentModal', {
            component: 'Withdraw',
            dataModal: {
              type: `deposit`,
              id: currentDepositsList[currentDepositIndex].id,
              balance: currentDepositsList[currentDepositIndex].balance,
            },
          })
        "
      >
        {{ loc("bank:menu:120") }}
      </div>
      <div
        class="bank-deposits__current-btn"
        @click="closeDeposit(currentDepositsList[currentDepositIndex].id)"
      >
        {{ loc("bank:menu:121") }}
      </div>
    </div>
    <div class="bank-deposits__current" v-else>
      <div class="bank-deposits__current-prompt">
        {{ loc("bank:menu:122") }}
      </div>
      <div class="bank-deposits__current-title">{{ loc("bank:menu:123") }}</div>
      <div class="bank-deposits__current__desc">{{ loc("bank:menu:124") }}</div>
      <div class="bank-deposits__current__desc-list">
        <div class="bank-deposits__current__desc-list-item replenishment">
          {{ loc("bank:menu:125") }}
        </div>
        <div class="bank-deposits__current__desc-list-item completion">
          {{ loc("bank:menu:126") }}
        </div>
      </div>
    </div> -->

    <div class="bank-deposits_list">
      <BankDepositsItem
        v-for="item in depositList"
        :key="item.Id"
        :item="item"
        @setModal="setModal"
      />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
import BankDepositsItem from "./BankDepositsItem";
import BankDepositsModal from "./BankDepositsModal";
import deposits from "../../../configs/bank/deposits";

export default {
  name: "BankDeposits",
  components: {
    BankDepositsItem,
    BankDepositsModal,
  },
  data() {
    return {
      modal: {
        show: false,
        dataModal: {},
      },
      currentDepositIndex: 0,
    };
  },
  computed: {
    ...mapState("bank/deposit", ["currentDepositsList"]),
    ...mapState("smartphone/bankPage", ["balance"]),
    ...mapGetters("localization", ["loc"]),
    depositList() {
      return deposits.filter(
        (item) => item.MinMoney <= this.balance && item.Actual
      );
    },
    currentDeposit() {
      return deposits.find(
        (element) =>
          element.Id ===
          this.currentDepositsList[this.currentDepositIndex].depositTypes
      );
    },
    getLeftTime() {
      let time = this.currentDepositsList[this.currentDepositIndex].time;
      if (time > 24) return `${Math.floor(time / 24)} e. ${time % 24} guide.`;
      else if (time > 0) return `${time % 24} guide.`;
      else return `Completed`;
    },
  },
  methods: {
    setModal(id) {
      this.modal.dataModal = deposits.find((element) => element.Id === id);
      this.modal.show = true;
    },
    closeModal() {
      this.modal.show = false;
      this.modal.dataModal = {};
    },
    closeDeposit(id) {
      window.mp.triggerServer("bank:closeDeposit", id);
    },
    prevDeposit() {
      if (this.currentDepositIndex > 0) {
        this.currentDepositIndex--;
      } else {
        this.currentDepositIndex = this.currentDepositsList.length - 1;
      }
    },
    nextDeposit() {
      if (this.currentDepositIndex < this.currentDepositsList.length - 1) {
        this.currentDepositIndex++;
      } else {
        this.currentDepositIndex = 0;
      }
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

.bank-deposits {
  width: 100%;
  height: 100%;
  padding: conv(53, true) conv(193) conv(116, true) conv(237);
  display: grid;
  grid-template-columns: conv(780) conv(744);
  column-gap: conv(10);

  &_current {
    width: 100%;
    height: 100%;
    display: flex;
  }

  &_items {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;

    &-wrap {
      width: 100%;
      border: 1.15px solid rgba(255, 255, 255, 0.09);
      padding: conv(54, true) 0 conv(33, true) conv(59);
      display: flex;
      align-items: center;
      background: linear-gradient(
        359.35deg,
        rgba(255, 255, 255, 0.04) 0.55%,
        rgba(255, 255, 255, 0) 99.44%
      );

      &_img {
        height: conv(221, true);
        width: conv(218, true);
        margin-right: conv(47);
        position: relative;

        img {
          &:first-child {
            width: 100%;
            height: 100%;
            opacity: 0.07;
            z-index: 1;
            position: relative;
          }

          &:last-child {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            height: conv(76, true);
            width: conv(75, true);
            z-index: 2;
          }
        }
      }

      &_content {
        display: flex;
        flex-direction: column;
      }

      &_info {
        position: relative;

        & > div {
          &:first-child {
            display: flex;
            align-items: center;
            font-weight: 700;
            font-size: conv(12, true);
            line-height: conv(14, true);
            text-transform: uppercase;
            color: rgba(255, 255, 255, 0.55);

            img {
              opacity: 0.2;

              &.active {
                opacity: 1;
              }

              &:not(:last-child) {
                margin-left: conv(10);
                width: conv(16, true);
                height: conv(16, true);
              }

              &:last-child {
                margin-left: conv(5);
                width: conv(18, true);
                height: conv(18, true);
              }
            }
          }

          &:nth-child(2) {
            margin-top: conv(1, true);
            font-weight: 700;
            font-size: conv(32, true);
            line-height: conv(38, true);
            text-transform: uppercase;
            color: #ffffff;
          }

          &:last-child {
            margin-top: conv(9, true);
            max-width: conv(219);
            font-weight: 500;
            font-size: conv(16, true);
            line-height: conv(19, true);
            text-transform: uppercase;
            color: #ffffff;
          }
        }

        &::after {
          content: "";
          position: absolute;
          top: 0;
          left: conv(-14);
          transform: translateX(-100%);
          background: #ffffff;
          width: conv(2);
          height: conv(26, true);
          box-shadow: 0px 0px conv(14, true) rgba(255, 255, 255, 0.55);
        }
      }

      &_bet {
        position: relative;
        margin-top: conv(16, true);

        &::after {
          content: "";
          position: absolute;
          top: 0;
          left: conv(-14);
          transform: translateX(-100%);
          width: conv(2);
          height: conv(26, true);
          background: #ffdb5c;
          box-shadow: 0px 0px SCHNTM(14, true) rgba(255, 219, 92, 0.55);
        }

        & > div {
          &:first-child {
            font-weight: 700;
            font-size: conv(14, true);
            line-height: conv(17, true);
            text-transform: uppercase;
            color: #ffdb5c;
          }

          &:last-child {
            font-weight: 700;
            font-size: conv(32, true);
            line-height: conv(38, true);
            text-transform: uppercase;
            color: #ffdb5c;
          }
        }
      }

      &_balance {
        &:not(:last-child) {
          margin-top: conv(36, true);
        }

        &:last-child {
          margin-top: conv(12, true);
        }

        & > div {
          &:first-child {
            font-weight: 700;
            font-size: conv(20, true);
            line-height: conv(24, true);
            text-transform: uppercase;
            color: rgba(255, 255, 255, 0.55);
          }

          &:last-child {
            font-weight: 700;
            font-size: conv(40, true);
            line-height: conv(48, true);
            text-transform: uppercase;
            color: #ffffff;
            display: flex;
            align-items: center;

            .price {
              margin-left: conv(19);
              font-weight: 700;
              font-size: conv(20, true);
              line-height: conv(24, true);
              text-align: center;
              text-transform: uppercase;
              color: #a0ff98;
            }
          }
        }
      }
    }

    &-bottom {
      margin-top: conv(20, true);
      width: 100%;
      display: flex;
      justify-content: space-between;
      align-items: flex-start;
    }

    &-btns {
      display: flex;
      flex-direction: column;

      button {
        display: flex;
        align-items: center;
        cursor: pointer;
        height: conv(73, true);
        font-weight: 700;
        color: white;
        text-transform: uppercase;
        white-space: nowrap;

        &.refill {
          background: rgba(39, 119, 56, 0.15);
          font-size: conv(20, true);
          line-height: conv(24, true);
          padding-left: conv(29);
          padding-right: conv(50);
          transition: 0.2s ease;

          &:hover {
            background: rgba(39, 119, 56, 0.3);
          }

          img {
            width: conv(29, true);
            height: conv(29, true);
            margin-right: conv(17);
          }
        }

        &.withdraw {
          justify-content: center;
          background: rgba(255, 255, 255, 0.05);
          font-size: conv(24, true);
          line-height: conv(29, true);
          text-align: center;
          transition: 0.2s ease;

          &:hover {
            background: rgba(255, 255, 255, 0.15);
          }
        }

        &:nth-child(2) {
          margin-top: conv(10, true);
        }
      }
    }

    &-scroll {
      display: flex;
      align-items: center;
      margin-left: conv(10);

      div {
        &:first-child {
          font-weight: 700;
          font-size: conv(24, true);
          line-height: conv(29, true);
          text-transform: uppercase;
          color: #ffffff;
          margin-right: conv(20);
        }

        &:last-child {
          display: grid;
          grid-template-columns: 1fr auto 1fr;

          & > button {
            cursor: pointer;
            display: flex;
            justify-content: center;
            align-items: center;
            background: rgba(255, 255, 255, 0.1);
            width: conv(66, true);
            height: conv(66, true);
            transition: 0.2s ease;

            img {
              height: conv(24, true);
            }

            &:last-child img {
              transform: rotate(180deg);
            }

            &:hover {
              background: rgba(255, 255, 255, 0.15);
            }
          }

          & > div {
            width: conv(81);
            height: conv(66, true);
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: 700;
            font-size: conv(24, true);
            line-height: conv(29, true);
            text-align: center;
            text-transform: uppercase;
            color: #ffffff;
          }
        }
      }
    }
  }

  &_empty {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;

    &-title {
      font-weight: 700;
      font-size: conv(64, true);
      line-height: conv(77, true);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: conv(23.47, true);
    }

    &-descr {
      height: conv(125.18, true);
      width: conv(618.45);
      font-weight: 700;
      font-size: conv(20, true);
      line-height: conv(24, true);
      text-align: center;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.44);
    }

    &-red {
      margin-top: conv(11.95, true);
      font-weight: 700;
      font-size: conv(36, true);
      line-height: conv(43, true);
      display: flex;
      align-items: center;
      text-align: center;
      text-transform: uppercase;
      color: #301934 ;
    }

    &-subdescr {
      margin-top: conv(10, true);
      font-weight: 700;
      font-size: conv(20, true);
      line-height: conv(44, true);
      display: flex;
      align-items: center;
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
    }
  }

  &_list {
    display: grid;
    grid-template-rows: 1fr 1fr 1fr;
    row-gap: conv(5, true);
    width: 100%;
    height: 100%;
  }
}

/* .bank-deposits {
  display: flex;
  align-items: flex-start;
  position: relative;
  z-index: 1;
  width: 70rem;
  &__current {
    width: 20rem;
    display: flex;
    flex-direction: column;
    letter-spacing: 0.03em;
    &-prompt {
      display: flex;
      align-items: center;
      font-weight: normal;
      font-size: 1.2rem;
      line-height: 1.45rem;
      margin-bottom: 0.4rem;
      position: relative;
      text-align: left;
      width: 100%;
      span {
        position: absolute;
        right: 0;
        font-weight: bold;
        font-size: 1.2rem;
        letter-spacing: 0.03em;
        color: #b6d300;
      }
    }
    &-title {
      font-weight: bold;
      font-size: 3rem;
      line-height: 3rem;
    }
    &-desc {
      font-weight: normal;
      font-size: 0.9rem;
      line-height: 1.2rem;
      color: rgba(255, 255, 255, 0.4);
      margin-bottom: 1.2rem;
    }
    &__circs {
      display: flex;
      align-items: center;
      padding-bottom: 2rem;
      border-bottom: 1px solid rgba(255, 255, 255, 0.2);
      margin-bottom: 1.9rem;
      position: relative;
      &-item {
        width: 2rem;
        height: 2rem;
        border: 0.1rem solid rgba(255, 255, 255, 0.3);
        box-sizing: border-box;
        border-radius: 0.2rem;
        margin-right: 0.5rem;
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        &:before {
          content: "";
          width: 1.1rem;
          height: 1.1rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          opacity: 0.3;
        }
        &.active {
          &:before {
            opacity: 1;
          }
        }
        &.replenishment {
          &:before {
            background-image: url("/img/bank/replenishment.png");
          }
        }
        &.completion {
          &:before {
            background-image: url("/img/bank/completion.png");
          }
        }
      }
      &__info {
        display: flex;
        flex-direction: column;
        margin-left: 1rem;
        font-weight: normal;
        &-value {
          font-size: 1.5rem;
          line-height: 1.8rem;
        }
        &-desc {
          font-size: 1rem;
          line-height: 1.2rem;
          color: rgba(255, 255, 255, 0.4);
        }
      }
    }
    &__info {
      display: flex;
      flex-direction: column;
      margin-bottom: 1.2rem;
      &-desc {
        font-weight: normal;
        font-size: 1rem;
        line-height: 1.2rem;
        color: rgba(255, 255, 255, 0.5);
      }
      &-balance,
      &-time {
        font-weight: bold;
        font-size: 2rem;
        line-height: 2rem;
      }
      &-profit {
        font-weight: normal;
        font-size: 1.3rem;
        line-height: 1.55rem;
        color: #b6d300;
        margin-bottom: 1.2rem;
      }
    }
    &-btn {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
      margin-bottom: 0.4rem;
      height: 4rem;
      min-width: 4rem;
      border: 0.1rem solid #ffffff;
      &.btn-main {
        &:before {
          content: "";
          background-image: url("/img/bank/replenishment-icon.png");
        }
        &.withdraw {
          &:before {
            background-image: url("/img/bank/withdrawal-icon.png");
          }
        }
      }
      &:last-child {
        margin-bottom: 0;
      }
      &:hover {
        background-color: rgba(255, 255, 255, 0.1);
      }
    }
    &__desc {
      width: 20rem;
      padding-left: 1.2rem;
      position: relative;
      font-weight: normal;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      margin-top: 0.35rem;
      margin-bottom: 1.9rem;
      &:before {
        content: "";
        width: 0.2rem;
        height: 100%;
        position: absolute;
        left: 0;
        top: 0;
        background: #5e37b0;
        box-shadow: 0 0.5rem 5rem rgba(94, 55, 176, 0.6);
        border-radius: 0.2rem;
      }
      &-list {
        display: flex;
        flex-direction: column;
        &-item {
          display: flex;
          align-items: center;
          background: linear-gradient(
            90deg,
            rgba(255, 255, 255, 0.2) 0%,
            rgba(255, 255, 255, 0) 100%
          );
          border-radius: 0.3rem;
          position: relative;
          height: 3.5rem;
          width: 20rem;
          padding: 0 1.25rem;
          font-weight: normal;
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          margin-bottom: 0.5rem;
          &:last-child {
            margin-bottom: 0;
          }
          &:before,
          &:after {
            content: "";
          }
          &:before {
            content: "";
            width: 2rem;
            height: 2rem;
            margin-right: 1rem;
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
          }
          &.replenishment {
            &:before {
              background-image: url("/img/bank/replenishment.png");
            }
          }
          &.completion {
            &:before {
              background-image: url("/img/bank/completion.png");
            }
          }
          &:after {
            width: 0.2rem;
            height: 2rem;
            background: #ffffff;
            box-shadow: 0 0.5rem 5rem rgba(94, 55, 176, 0.6);
            border-radius: 0.2rem;
            position: absolute;
            left: 0;
          }
        }
      }
    }
    &__btn {
      width: 3rem;
      height: 3rem;
      position: absolute;
      bottom: 0;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url("/img/bank/prev.png");
      opacity: 0.7;
      transition: 0.2s;
      &:hover {
        opacity: 1;
      }
      &.prev {
        left: -1rem;
        transform: translateX(-100%) translateY(50%);
      }
      &.next {
        transform-origin: 50% 50%;
        right: -1rem;
        transform: translateX(100%) translateY(50%) rotate(180deg);
      }
    }
  }
  &__list {
    width: 43rem;
    height: calc(100vh - (100vh - (43rem)) / 2);
    overflow-y: auto;
    position: absolute;
    right: 0;
    top: 0;
    padding-right: 1.3rem;
    &::-webkit-scrollbar {
      width: 0.3rem;
      background-color: transparent;
      &-thumb {
        background-color: rgba(255, 255, 255, 0.3);
        border-radius: 0.5rem;
      }
    }
  }
} */
</style>
