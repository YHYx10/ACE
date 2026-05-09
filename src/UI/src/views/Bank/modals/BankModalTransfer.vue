<template>
  <div class="bank-transfer bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
       BACK
      </button>

      <div class="bank-modal_title">Medium transmission</div>
      <div class="bank-modal_descr">
   Transfer to another account.Enter the account number and the amount
      </div>

      <div class="bank-modal_input">
        <div>Account number of the recipient</div>
        <div>
          <span>#</span>
          <input type="number" placeholder="number..." v-model="targetNumber" />
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

      <!-- <template v-if="currentPage === 0">
        <div class="bank-modal-title">{{ loc("bank:menu:15") }}</div>
        <div class="bank-modal-desc">{{ loc("bank:menu:16") }}</div>
        <div class="bank-transfer__info bank-modal__block-shadow cash">
          <div class="bank-modal-prompt">{{ loc("bank:menu:17") }}</div>
          <div class="bank-transfer__balance">
            $ {{ dataModal.balance.toLocaleString("ru") }}
          </div>
        </div>
        <div class="bank-modal-prompt card">{{ loc("bank:menu:18") }}</div>
        <div class="bank-transfer__input bank-modal__input">
          <input type="number" v-model="targetNumber" />
        </div>
        <div class="bank-modal-prompt">{{ loc("bank:menu:19") }}</div>
        <div class="bank-transfer__input bank-modal__input cash">
          <input type="number" v-model.number="transferAmount" />
        </div>

        <div class="bank-transfer__input bank-modal__input">
          <input
            type="text"
            v-model="transferComment"
            :placeholder="loc('bank:menu:20')"
          />
        </div>

        <template v-if="transfersPerDay + transferAmount > dailyTransferLimit">
          <div class="bank-transfer__error">
            {{ loc(`bank:menu:21@${dailyTransferLimit.toLocaleString("ru")}`) }}
          </div>
          <div class="bank-modal-btn btn-main" @click="setReason">
            {{ loc("bank:menu:22") }}
          </div>
        </template>

        <template v-else>
          <div class="bank-modal-btn btn-main" @click="transfer">
            {{ loc("bank:menu:23") }}
          </div>
        </template>
      </template>

      <template v-if="currentPage === 1">
        <BtnBack class="bank-transfer__back" @click="setPage(0)" />
        <div class="bank-modal-title">{{ loc("bank:menu:24") }}</div>
        <div class="bank-modal-desc">
          {{ loc(`bank:menu:25@${dailyTransferLimit.toLocaleString("ru")}`) }}
        </div>
        <div class="bank-transfer__fixed-transfer">
          <div class="bank-modal-desc">{{ loc("bank:menu:26") }}</div>
          <div class="bank-transfer__fixed-transfer-value">
            $ {{ transferAmount.toLocaleString("ru") }}
          </div>
        </div>
        <textarea
          class="bank-transfer__desc"
          :placeholder="loc('bank:menu:27')"
          v-model="currentReason"
        ></textarea>
        <div class="bank-modal-btn btn-main" @click="sendRequest">
          {{ loc("bank:menu:28") }}
        </div>
      </template>

      <template v-if="currentPage === 2">
        <div class="bank-modal-title">{{ loc("bank:menu:29") }}</div>
        <div class="bank-modal-desc">{{ loc("bank:menu:30") }}</div>
        <div class="bank-modal-btn btn-main" @click="$emit('closeModal')">
          {{ loc("bank:menu:31") }}
        </div>
      </template> -->
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
/* import BtnBack from "./components/BtnBack"; */
export default {
  name: "BankModalTransfer",
  components: {
    /* BtnBack, */
  },
  props: {
    dataModal: Object,
  },
  data() {
    return {
      currentPage: 0,
      targetNumber: null,
      transferAmount: null,
      transferComment: "",
      currentReason: "",
    };
  },
  computed: {
    ...mapState("smartphone/bankPage", ["balance"]),
    ...mapState("bank", ["transfersPerDay", "dailyTransferLimit"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    transfer() {
      if (this.targetNumber && this.transferAmount && this.transferAmount > 0) {
        window.mp.triggerServer(
          "bank:transfer",
          this.targetNumber,
          this.transferAmount,
          this.transferComment,
          null
        );
        this.$emit("closeModal");
      }
    },
    setReason() {
      if (this.targetNumber && this.transferAmount > 0) {
        this.setPage(1);
      }
    },
    setPage(page) {
      this.currentPage = page;
    },
    sendRequest() {
      if (this.currentReason && this.targetNumber) {
        window.mp.triggerServer(
          "bank:transfer",
          this.targetNumber,
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

.bank-modal_comment {
  margin: conv(30, true) 0 conv(20, true);
  width: 100%;
  height: conv(93, true);
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(217, 217, 217, 0.05);

  input {
    position: relative;
    z-index: 100;
    width: 100%;
    height: 100%;
    text-transform: uppercase;
    color: white;
    background: none;
    border: none;
    outline: none;

    &,
    &::placeholder {
      font-weight: 700;
      font-size: conv(14, true);
      line-height: conv(17, true);
      text-align: center;
      color: rgba(255, 255, 255, 0.5);
    }
  }
}

.bank-transfer {
  .bank-modal_wrap {
    padding: conv(34, true) conv(97) conv(40, true);
    width: conv(504);
  }

  .bank-modal_close {
    margin-bottom: conv(16, true);
  }

  .bank-modal_title {
    margin-bottom: conv(10, true);
  }

  .bank-modal_descr {
    margin-bottom: conv(34, true);
  }

  .bank-modal_input {
    margin-bottom: conv(20, true);

    &:nth-child(4) {
      margin-bottom: conv(10, true);
    }
  }
}
/* .bank-transfer {
  .bank-modal-title {
    margin-bottom: 0.5rem;
  }
  &__back {
    margin-bottom: 0.5rem;
  }
  &__wrap {
  }
  &__info {
    margin-bottom: 2rem;
  }
  &__balance {
    font-weight: normal;
    font-size: 1.6rem;
    line-height: 1.9rem;
    letter-spacing: 0.03em;
    color: #000000;
  }
  &__input {
    &.cash {
      &:before {
        content: "$";
        pointer-events: none;
        position: absolute;
        left: 0;
      }
      input {
        padding-left: 1.5rem;
      }
    }
  }
  &__error {
    font-weight: normal;
    font-size: 1rem;
    line-height: 1.1rem;
    letter-spacing: 0.03em;
    color: #ee443a;
    margin-bottom: 1.5rem;
  }
  &__fixed-transfer {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 1.2rem 0;
    border-top: 1px solid rgba(0, 0, 0, 0.1);
    border-bottom: 1px solid rgba(0, 0, 0, 0.1);
    margin-bottom: 1rem;
    .bank-modal-desc {
      margin-bottom: 0.3rem;
    }
    &-value {
      font-weight: normal;
      font-size: 2.5rem;
      line-height: 3rem;
      letter-spacing: 0.03em;
      color: #000000;
    }
  }
  &__desc {
    padding: 1rem;
    background: #ffffff;
    box-shadow: 0 0.5rem 3rem rgba(49, 41, 184, 0.2);
    border-radius: 0.5rem;
    height: 8rem;
    resize: none;
    font-size: 1rem;
    line-height: 1.2rem;
    letter-spacing: 0.03em;
    color: #5e37b0;
    border: none;
    outline: none;
    margin-bottom: 1.5rem;
    &::placeholder {
      color: #5e37b0;
    }
  }
} */
</style>
