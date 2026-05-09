<template>
  <div class="page">
    <div class="page__title">{{ loc("cityHallWeb_122") }}</div>
    <div class="current-name">
      <div>The current name</div>
      <div>{{ name }}</div>
    </div>
    <div class="new-name">
      <div>Enter a new name</div>
      <div :class="[{ error: !nameIsFree }, 'input-wrap']">
        <input :placeholder="loc('cityHallWeb_137')" v-model="currentName" />
        <div class="input-wrap__error" v-if="!nameIsFree">{{ textError }}</div>
        <div class="input-wrap_price" v-if="currentName && nameIsFree">
          <div>PRICE</div>
          <div>${{ currentCost.toLocaleString("ru-RU") }}</div>
        </div>
      </div>
    </div>
    <div class="btns">
      <div
        v-if="currentName && nameIsFree"
        class="name-btn bank-btn"
        @click="sendForm"
      >
        <span>Pay</span>
      </div>
      <Payment
        v-if="currentName && nameIsFree"
        :currentPayment="currentPayment"
        :setCurrentPayment="setCurrentPayment"
      />
    </div>
  </div>
</template>

<script>
import Payment from "../../components/Payment";
import { mapGetters, mapState } from "vuex";
export default {
  name: "NameEditPage",

  components: {
    Payment,
  },

  data: function () {
    return {
      currentName: "",
      currentPayment: 0, // 0 - money , 1 - card
    };
  },

  methods: {
    sendForm: function () {
      window.mp.trigger(
        "cityHallWeb:sendFormNameEditPage",
        this.currentName,
        this.currentPayment
      );
      if (this.nameIsFree) {
        this.currentName = null;
      }
      this.currentPayment = 0;
    },

    setCurrentPayment: function (value) {
      this.currentPayment = value;
    },
  },

  computed: {
    ...mapState("cityHallWeb/nameEdit", [
      "nameIsFree",
      "textError",
      "currentCost",
    ]),
    ...mapState("cityHallWeb", ["cardId", "name"]),
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.city-hall-web {
  .body-tab {
    .page {
      &__title {
        margin-bottom: conv(36);
      }

      .name-btn {
        display: flex;
        justify-content: center;
        align-items: center;
        background: linear-gradient(
          180deg,
          #301934  0%,
          #591b87 100%,
          #591b87 100%
        );
        width: conv(342);
        height: conv(75);
        margin: conv(15) 0 conv(20);
        cursor: pointer;
        font-weight: 700;
        font-size: conv(24);
        line-height: conv(29);
        text-align: center;
        text-transform: uppercase;
        color: #ffffff;
        border-radius: 0;
      }
    }

    .btns{
      display: flex;
      flex-direction: column;
      align-items: center;
      width: conv(342);
    }

    .current-name {
      margin-bottom: conv(26);

      div {
        &:first-child {
          color: rgba(255, 255, 255, 0.5);
        }

        &:last-child {
          width: conv(342);
          height: conv(89);
          background: rgba(255, 255, 255, 0.03);
          display: flex;
          align-items: center;
          padding-left: conv(20);
          font-weight: 700;
          font-size: conv(24);
          line-height: conv(29);
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }

    .current-name,
    .new-name {
      display: flex;
      flex-direction: column;

      & > div {
        &:first-child {
          padding-left: conv(20);
          font-weight: 600;
          font-size: conv(20);
          line-height: conv(42);
          text-transform: uppercase;
        }
      }
    }

    .new-name {
      & > div:first-child {
        color: #ffffff;
      }
    }

    .input-wrap {
      position: relative;
      width: conv(342);
      height: conv(89);
      background: rgba(0, 0, 0, 0.2);
      display: flex;
      align-items: center;

      input {
        border: none;
        margin: 0;
        width: 100%;
        z-index: 1;
        padding-left: conv(20);
        outline: none;
        background: none;
        color: white;

        &,
        &::placeholder {
          font-weight: 700;
          font-size: conv(20);
          line-height: conv(24);
          text-transform: uppercase;
        }

        &::placeholder {
          color: rgba(255, 255, 255, 0.2);
        }
      }
      &.error {
        border-radius: 0.3rem 0.3rem 0 0;
        border-bottom: 0.1rem solid #ff3838;
        input {
          padding: 0 2.6rem 0 1rem;
        }
        .input-wrap__error,
        &::after {
          opacity: 1;
        }
      }
      &_error,
      &:after {
        opacity: 0;
      }
      &__error {
        position: absolute;
        left: 0;
        bottom: -0.3rem;
        transform: translateY(100%);
        font-family: Roboto;
        font-style: normal;
        font-weight: normal;
        font-size: 0.8rem;
        line-height: 0.9rem;
        color: #e00b29;
      }
      &:after {
        content: "";
        width: 1.25rem;
        height: 1.25rem;
        position: absolute;
        right: 1.1rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        background-image: url("/img/cityHallWeb/icon-error.svg");
        z-index: 0;
      }

      &_price {
        position: absolute;
        left: calc(100% + 2.1rem);
        top: 50%;
        transform: translateY(-50%);
        display: flex;
        flex-direction: column;

        div {
          &:first-child {
            font-weight: 700;
            font-size: conv(16);
            line-height: conv(19);
            text-transform: uppercase;
            color: #ffffff;
            margin-bottom: conv(3);
          }

          &:last-child {
            font-weight: 700;
            font-size: conv(32);
            line-height: conv(38);
            text-transform: uppercase;
            color: #a0ff98;
          }
        }
      }
    }
  }
}
</style>
