<template>
  <div class="bank-modal bank-deposits_modal">
    <div class="bank-modal_wrap">
      <div class="bank-deposits_modal-header">
        <button class="bank-modal_close" @click="$emit('closeModal')">
          <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
     BACK
        </button>

        <div>The discovery of a new contribution</div>
      </div>

      <div class="bank-deposits_item">
        <div class="bank-deposits_item-img">
          <img :src="`/img/bank/deposits/${dataModal.Image}.png`" alt="" />
          <img :src="`/img/bank/deposits/${dataModal.Image}.png`" alt="" />
        </div>

        <div class="bank-deposits_item-content">
          <div class="bank-deposits_item-info">
            <div>
        Information about the post
              <img
                :class="{ active: dataModal.IsRefill }"
                src="/img/bank/deposits/refill.svg"
                alt=""
              />
              <img
                :class="{ active: dataModal.IsWithdraw }"
                src="/img/bank/deposits/withdraw.svg"
                alt=""
              />
            </div>

            <div>{{ loc(dataModal.Title) }}</div>
            <div>{{ loc(dataModal.Description) }}</div>
          </div>

          <div class="bank-deposits_item-bet">
            <div
              v-for="(elem, index) in [
                ['An annual price', dataModal.AnnualRate + '%'],
                ['Me.summa', dataModal.MinMoney],
                ['The deposit time', loc(`bank:menu:141@${dataModal.MaxDays}`)],
              ]"
              :key="index"
            >
              <div>{{ elem[0] }}</div>
              <div>
                {{ index === 1 ? elem[1].toLocaleString("ru") + "$" : elem[1] }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="bank-deposits_modal-text">
        <div>Calculate your income </div>
        <div>
Be careful: Income is only included in the account when determining the account
Condition
        </div>
      </div>

      <div class="bank-deposits_modal-price">
        <div>
          <div class="bank-deposits_modal-price_range">
            <div>The amount of the contribution</div>
            <div>
              <div>
                <span>$</span>
                <input type="number" v-model="depositSumm" />
              </div>
              <range-slider
                :min="dataModal.MinMoney"
                :max="getMaxAmount"
                step="1000"
                v-model.number="depositSumm"
              />
            </div>
          </div>
          <div class="bank-deposits_modal-price_range">
            <div>The deposit time</div>
            <div>
              <div>
                <div>{{ loc(`bank:menu:135@${depositTerm}`) }}</div>
                <div>
                  {{
                    loc(
                      `bank:menu:134@${dataModal.MinDays}@${dataModal.MaxDays}`
                    )
                  }}
                </div>
              </div>
              <range-slider
                :min="dataModal.MinDays"
                :max="dataModal.MaxDays"
                step="1"
                v-model.number="depositTerm"
              />
            </div>
          </div>
        </div>
        <div>
          <div>
            <div>
              <div>Amount until the end of the term</div>
              <div>${{ summByEndTerm.toLocaleString("ru") }}</div>
            </div>
            <div>
              <div>An annual price</div>
              <div>{{ dataModal.AnnualRate }}%</div>
            </div>
          </div>
          <button @click="openDeposit" class="bank-btn">Open a post</button>
        </div>
      </div>
    </div>
  </div>
  <!-- <div class="bank-deposits-modal">
    <div class="bank-deposits-modal__wrap">
      <div 
        class="btn-close"
        @click="$emit('closeModal')"
      ></div>
      <div
        class="bank-deposits-modal-info"
        :style="{ backgroundImage: `url(/img/bank/deposits/${dataModal.Image}.png)` }"
      >
        <div class="bank-deposits-modal-title">{{ loc(dataModal.Title) }}</div>
        <div class="bank-deposits-modal-desc">{{ loc(dataModal.Description) }}</div>
        <div class="bank-deposits-modal__info">
          <div class="bank-deposits-modal__info-col">
            <div class="bank-deposits-modal__info-value">
              {{ dataModal.AnnualRate }}%
            </div>
            <div class="bank-deposits-modal__info-desc">{{loc('bank:menu:127')}}</div>
          </div>
          <div class="bank-deposits-modal__info-col">
            <div class="bank-deposits-modal__info-value">
              {{ dataModal.MinMoney.toLocaleString("ru") }}$
            </div>
            <div class="bank-deposits-modal__info-desc">{{loc('bank:menu:128')}}</div>
          </div>
          <div class="bank-deposits-modal__info-col">
            <div class="bank-deposits-modal__info-value">
              to {{ dataModal.MaxDays }} e.
            </div>
            <div class="bank-deposits-modal__info-desc">{{loc('bank:menu:129')}}</div>
          </div>
        </div>
        <div class="bank-deposits-modal__provisions">
          <div :class="[{ active: dataModal.IsRefill }, 'bank-deposits-modal-provision', 'replenishment']">
            <div class="bank-prompt">{{dataModal.IsRefill ? loc('bank:menu:112') : loc('bank:menu:113')}}</div>
          </div>
          <div :class="[{ active: dataModal.IsWithdraw }, 'bank-deposits-modal-provision', 'completion']">
            <div class="bank-prompt">{{dataModal.IsWithdraw ? loc('bank:menu:114') : loc('bank:menu:115')}}</div>
          </div>
        </div>
      </div>
      <div class="bank-deposits-modal-interaction">
        <div class="bank-deposits-modal-title">{{loc('bank:menu:130')}}</div>
        <div class="bank-deposits-modal-desc">{{loc('bank:menu:131')}}</div>
        <div class="bank-deposits-modal-interaction__calculation">
          <div>
            <div class="bank-deposits-modal-interaction__calculation__range">
              <div class="bank-deposits-modal-interaction__calculation__range-title">{{loc('bank:menu:132')}}</div>
              <div class="bank-deposits-modal-interaction__calculation__range-wrap">
                <div class="bank-deposits-modal-interaction__calculation__range-value">{{depositSumm}}</div>
                <div class="bank-deposits-modal-interaction__calculation__range-desc">$</div>
                <range-slider
                  :min="dataModal.MinMoney"
                  :max="getMaxAmount"
                  step="1000"
                  v-model.number="depositSumm"
                />
              </div>
            </div>
            <div class="bank-deposits-modal-interaction__calculation__range">
              <div class="bank-deposits-modal-interaction__calculation__range-title">{{loc('bank:menu:133')}}</div>
              <div class="bank-deposits-modal-interaction__calculation__range-wrap">
                <div class="bank-deposits-modal-interaction__calculation__range-value">{{loc(`bank:menu:135@${depositTerm}`)}}</div>
                <div class="bank-deposits-modal-interaction__calculation__range-desc">{{loc(`bank:menu:134@${dataModal.MinDays}@${dataModal.MaxDays}`)}}</div>
                <range-slider
                  :min="dataModal.MinDays"
                  :max="dataModal.MaxDays"
                  step="1"
                  v-model.number="depositTerm"
                />
              </div>
            </div>
          </div>
          <div>
            <div class="bank-deposits-modal-interaction__calculation__display">
              <div class="bank-deposits-modal-interaction__calculation__display-desc">{{loc('bank:menu:136')}}</div>
              <div class="bank-deposits-modal-interaction__calculation__display-value">{{summByEndTerm.toLocaleString('ru')}} $</div>
              <div class="bank-deposits-modal-interaction__calculation__display-prompt">{{loc(`bank:menu:137@${dataModal.AnnualRate}`)}}</div>
              <div 
                class="bank-deposits-modal-interaction__calculation__display-btn btn-main"
                @click="openDeposit"
              >{{loc('bank:menu:138')}}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div> -->
</template>

<script>
import RangeSlider from "vue-range-slider";
import "vue-range-slider/dist/vue-range-slider.css";
import { mapState, mapGetters } from "vuex";
export default {
  name: "BankDepositsModal",
  props: {
    dataModal: Object,
  },
  components: {
    RangeSlider,
  },
  data() {
    return {
      depositSumm: 0,
      depositTerm: 0,
    };
  },
  computed: {
    ...mapState("smartphone/bankPage", ["balance"]),
    ...mapGetters("localization", ["loc"]),
   // considers the total amount by the end of the term, made an example of counting
    summByEndTerm() {
      return Math.round(
        this.depositSumm *
          (1 + ((this.depositTerm / 360) * this.dataModal.AnnualRate) / 100)
      );
    },
    getMaxAmount() {
      return this.dataModal.MaxMoney < this.balance
        ? this.dataModal.MaxMoney
        : this.balance;
    },
  },
  methods: {
    openDeposit() {
      if (this.balance > this.dataModal.MinMoney) {
        window.mp.triggerServer(
          "bank:openDeposit",
          this.dataModal.Id,
          this.depositSumm,
          this.depositTerm
        );
      }
    },
  },
  mounted() {
    this.depositSumm = this.dataModal.MinMoney;
    this.depositTerm = this.dataModal.MinDays;
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

.bank-deposits_modal {
  .bank-modal_wrap {
    width: conv(804);
    padding: conv(24, true) conv(30) conv(31, true);
  }

  &-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    margin-bottom: conv(20, true);

    & > div:last-child {
      font-weight: 700;
      font-size: conv(24, true);
      line-height: conv(47, true);
      color: #ffffff;
    }
  }

  .bank-deposits_item {
    height: auto;
    padding-top: conv(26, true);
    padding-bottom: conv(27, true);
    margin-bottom: conv(19, true);
  }

  &-text {
    padding-left: conv(58);
    margin-bottom: conv(14, true);

    div {
      text-transform: uppercase;
      color: #ffffff;
      font-weight: 700;

      &:first-child {
        font-size: conv(20, true);
        line-height: conv(24, true);
        margin-bottom: conv(5, true);
      }

      &:last-child {
        font-weight: 500;
        font-size: conv(15, true);
        line-height: conv(18, true);
      }
    }
  }

  &-price {
    display: grid;
    grid-template-columns: conv(310) conv(310);
    column-gap: conv(44);
    align-items: flex-end;

    & > div {
      &:first-child {
        width: 100%;
        display: flex;
        flex-direction: column;
      }

      &:last-child {
        display: flex;
        flex-direction: column;
        width: 100%;
        justify-content: flex-end;

        & > div {
          width: 100%;
          display: flex;
          flex-direction: column;
          align-items: center;
          padding: conv(13, true) 0;
          background: rgba(217, 217, 217, 0.05);

          & > div {
            display: flex;
            flex-direction: column;
            align-items: center;

            div {
              text-transform: uppercase;
              color: #ffffff;
              font-weight: 700;

              &:first-child {
                font-size: conv(14, true);
                line-height: conv(17, true);
              }

              &:last-child {
                font-size: conv(24, true);
                line-height: conv(29, true);
              }
            }

            &:first-child {
              margin-bottom: conv(11, true);

              div:last-child {
                color: #a0ff98;
              }
            }
          }
        }

        & > button {
          display: flex;
          justify-content: center;
          align-items: center;
          width: 100%;
          height: conv(75, true);
          // background: linear-gradient(180deg, #db121b 0%, #591b87 100%);
          font-weight: 700;
          font-size: conv(24, true);
          line-height: conv(29, true);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }

    &_range {
      &:first-child {
        margin-bottom: conv(11, true);
      }

      & > div {
        &:first-child {
          margin-bottom: conv(4, true);
          font-weight: 700;
          font-size: conv(16, true);
          line-height: conv(19, true);
          text-transform: uppercase;
          color: #ffffff;
        }

        &:last-child {
          display: flex;
          flex-direction: column;
          width: 100%;

          & > div {
            &:first-child {
              width: 100%;
              background: rgba(217, 217, 217, 0.05);
              position: relative;
              height: conv(65, true);
              display: flex;
              align-items: center;
              justify-content: space-between;

              span {
                width: conv(49);
                height: 100%;
                font-weight: 700;
                font-size: conv(24, true);
                line-height: conv(29, true);
                display: flex;
                align-items: center;
                text-align: center;
                text-transform: uppercase;
                color: #ffffff;
              }

              input {
                background: none;
                border: none;
                outline: none;
                width: 100%;
                margin-left: conv(2);
                height: 100%;
                color: white;

                &,
                &::placeholder {
                  font-weight: 700;
                  font-size: conv(14, true);
                  line-height: conv(17, true);
                  text-transform: uppercase;
                }

                &::placeholder {
                  color: rgba(255, 255, 255, 0.5);
                }
              }

              div {
                text-transform: uppercase;
                color: #ffffff;
                font-weight: 700;

                &:first-child {
                  padding-left: conv(18);
                  font-size: conv(24, true);
                  line-height: conv(29, true);
                }

                &:last-child {
                  padding-right: conv(10);
                  font-size: conv(16, true);
                  line-height: conv(19, true);
                  opacity: 0.3;
                }
              }
            }

            &:last-child {
              font-weight: 700;
              font-size: conv(16, true);
              line-height: conv(19, true);
              text-transform: uppercase;
              color: #ffffff;
              opacity: 0.3;
            }
          }

          & > span {
            display: block;
            width: 100%;
            position: relative;
            left: auto;
            bottom: auto;
            border-radius: 0;

            .range-slider-fill {
              background: #ee0f19;
              box-shadow: none;
            }

            .range-slider-knob {
              background: #ffffff;
              box-shadow: 0px conv(4, true) conv(17, true) rgba(255, 255, 255, 0.55);
            }
          }
        }
      }
    }
  }
}

/* .bank-deposits-modal {
  width: 100vw;
  height: 100vh;
  position: fixed;
  left: 0;
  top: 0;
  z-index: 1;
  background: radial-gradient(
      43.85% 77.96% at 50% 50%,
      #4e6ed8 0%,
      rgba(78, 110, 216, 0) 100%
    ),
    rgba(0, 0, 0, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  &__wrap {
    width: 43rem;
    height: 43.5rem;
    background: #030c20;
    border-radius: 1rem;
    display: flex;
    flex-direction: column;
    position: relative;
  }
  &-info {
    display: flex;
    flex-direction: column;
    width: 100%;
    padding: 2.5rem 0 0 2rem;
    position: relative;
    height: 16.5rem;
    background-repeat: no-repeat;
    background-size: contain;
    background-position: right;
    margin-bottom: 3.8rem;
    &:after {
      content: "";
      width: calc(100% - (2 * (2rem)));
      height: 1px;
      background-color: rgba(255, 255, 255, 0.2);
      position: absolute;
      left: 2rem;
      bottom: -1.5rem;
    }
  }
  &__provisions {
    display: flex;
    align-items: center;
  }
  &-interaction {
    display: flex;
    flex-direction: column;
    padding: 0 2rem;
    .bank-deposits-modal-desc {
      max-width: 25rem;
      margin-bottom: 1.5rem;
    }
    &__calculation {
      display: flex;
      align-items: flex-start;
      justify-content: space-between;
      & > div {
        display: flex;
        flex-direction: column;
        &:nth-child(1) {
          height: 100%;
          justify-content: space-between;
        }
      }
      &__range {
        display: flex;
        flex-direction: column;
        position: relative;
        font-size: 1.5rem;
        letter-spacing: 0.03em;
        margin-bottom: 3.5rem;
        &:last-child {
          margin-bottom: 0;
        }
        &-title {
          font-weight: normal;
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          color: rgba(255, 255, 255, 0.4);
          margin-bottom: 0.2rem;
        }
        &-wrap {
          background: rgba(255, 255, 255, 0.1);
          border-radius: 0.4rem;
          padding: 0 1.5rem;
          display: flex;
          align-items: center;
          justify-content: space-between;
          width: 100%;
          height: 4rem;
          width: 22rem;
        }
        &-desc {
          color: rgba(255, 255, 255, 0.5);
          font-weight: normal;
        }
        &-value {
          font-weight: bold;
        }
      }
      &__display {
        width: 15rem;
        height: 14rem;
        background: rgba(255, 255, 255, 0.1);
        border-radius: 0.4rem;
        padding: 1.5rem 2rem;
        letter-spacing: 0.03em;
        &-desc,
        &-prompt {
          font-size: 1.1rem;
          line-height: 1.2rem;
          color: rgba(255, 255, 255, 0.4);
        }
        &-desc {
          margin-bottom: 0.2rem;
        }
        &-value {
          font-weight: normal;
          font-size: 2.3rem;
          line-height: 2.75rem;
          margin-bottom: 0.7rem;
        }
        &-prompt {
          font-weight: normal;
          margin-bottom: 1.4rem;
          span {
            color: #fff;
            font-weight: bold;
          }
        }
        &-btn {
          width: 100%;
          height: 3rem;
        }
      }
    }
  }
  &-title {
    font-weight: bold;
    font-size: 2rem;
    line-height: 2rem;
    letter-spacing: 0.03em;
    margin-bottom: 0.2rem;
  }
  &-desc {
    font-size: 1rem;
    line-height: 1.2rem;
    letter-spacing: 0.03em;
    color: rgba(255, 255, 255, 0.4);
    margin-bottom: 2.15rem;
  }
  &__info {
    display: flex;
    align-items: center;
    margin-bottom: 1.25rem;
    &-col {
      display: flex;
      flex-direction: column;
      margin-right: 2.7rem;
    }
    &-value {
      font-weight: normal;
      font-size: 1.5rem;
      line-height: 1.8rem;
    }
    &-desc {
      font-weight: normal;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      color: rgba(255, 255, 255, 0.4);
    }
  }
  &-provision {
    width: 2rem;
    height: 2rem;
    border: 0.1rem solid rgba(255, 255, 255, 0.3);
    box-sizing: border-box;
    border-radius: 0.2rem;
    margin-right: 1rem;
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
} */
</style>
