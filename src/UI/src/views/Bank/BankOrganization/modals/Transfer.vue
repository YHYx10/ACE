<template>
  <div class="bank-transfer bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
    BACK
      </button>

      <div class="bank-modal_title">Interfractional -transaction</div>

      <div class="bank-modal_input bank-modal_select">
        <div>Account number of the recipient </div>
        <div>
          <div @click="() => (dropOpen = !dropOpen)">
            <div>{{ fractionList[activeType][activeItem] }}</div>
            <img
              src="/img/bank/org/arrow.svg"
              alt=""
              :style="{
                transform: dropOpen ? 'rotate(180deg)' : 'rotate(0deg)',
              }"
            />
          </div>
          <div class="drop" v-if="dropOpen">
            <div>
              <div class="drop-title">Condition</div>
              <div class="drop-list">
                <div
                  v-for="(elem, index) in fractionList[0]"
                  :key="index"
                  :class="{
                    active: elem === fractionList[activeType][activeItem],
                  }"
                  @click="() => changeActiveFraction(0, index)"
                >
                  {{ elem }}
                </div>
              </div>
            </div>
            <div>
              <div class="drop-title">illegal</div>
              <div class="drop-list">
                <div
                  v-for="(elem, index) in fractionList[1]"
                  :key="index"
                  :class="{
                    active: elem === fractionList[activeType][activeItem],
                  }"
                  @click="() => changeActiveFraction(1, index)"
                >
                  {{ elem }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="bank-modal_input">
        <div>Enter the transfer amount</div>
        <div>
          <span>$</span>
          <input
            type="number"
            placeholder="Sum..."
            v-model="transferAmount"
          />
        </div>
      </div>

      <div class="bank-modal_comment">
        <input
          type="text"
          placeholder="Enter a comment..."
          v-model="transferComment"
        />
      </div>

      <button class="bank-modal_action bank-btn" @click="transfer">Transfer</button>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  name: "Transfer",
  props: {
    fractionList: Array,
  },
  data() {
    return {
      currentPage: 0,
      transferAmount: null,
      transferComment: "",
      currentReason: "",
      dropOpen: false,
      activeType: 0,
      activeItem: 0,
    };
  },
  computed: {
    ...mapState("smartphone/bankPage", ["balance"]),
    ...mapState("bank", ["transfersPerDay", "dailyTransferLimit"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    changeActiveFraction(type, name) {
      this.activeType = type;
      this.activeItem = name;
    },
    transfer() {
      if (this.transferAmount && this.transferAmount > 0) {
        window.mp.triggerServer(
          "bank:transferFraction",
          this.fractionList[this.activeType][this.activeItem],
          this.transferAmount,
          this.transferComment,
          null
        );
        this.$emit("closeModal");
      }
    },
    setReason() {
      if (this.transferAmount > 0) {
        this.setPage(1);
      }
    },
    setPage(page) {
      this.currentPage = page;
    },
    sendRequest() {
      if (this.currentReason) {
        window.mp.triggerServer(
          "bank:transferFraction",
          this.fractionList[this.activeType][this.activeItem],
          this.transferAmount,
          this.transferComment,
          this.currentReason
        );
        this.setPage(2);
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

.bank-modal {
  &_input.bank-modal_select {
    z-index: 120;

    * {
      z-index: 120;
    }

    & > div:last-child {
      position: relative;
      width: 100%;
      height: conv(65, true);
      position: relative;
      background: #1c1e20;

      & > div:first-child {
        width: 100%;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: space-between;
        background: #1c1e20;
        cursor: pointer;
        padding: 0 conv(20);

        div {
          display: block;
          white-space: nowrap;
          font-weight: 700;
          font-size: conv(20, true);
          line-height: conv(24, true);
          text-transform: uppercase;
          color: #ffffff;
        }

        img {
          height: conv(25, true);
          width: conv(15, true);
        }
      }

      .drop {
        position: absolute;
        top: 100%;
        left: 50%;
        width: 100%;
        transform: translateX(-50%);
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        background: #1c1e20;
        padding: conv(12, true) 0 conv(7, true) conv(20);
        z-index: 200;

        & div {
          text-align: left;
        }

        &-title {
          font-weight: 700;
          font-size: conv(16, true);
          line-height: conv(30, true);
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.5);
        }

        &-list {
          div {
            font-weight: 700;
            font-size: conv(20, true);
            line-height: conv(30, true);
            text-transform: uppercase;
            color: white;

            &.active {
              color: #ffcccc;
            }
          }
        }
      }
    }
  }
}
</style>
