<template>
  <div class="bank-main_fines bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
     BACK
      </button>
      <div class="bank-modal_title">Payment of fines</div>
      <div class="bank-main_fines-list" v-if="finesList.length > 0">
        <div
          class="bank-main_fines-item"
          v-for="item in finesList"
          :key="item.id"
        >
          <div>
            <div class="bank-main_fines-item_reason">
              {{ loc(item.reason) }}
            </div>
            <div class="bank-main_fines-item_info">
              <div>${{ item.cost.toLocaleString("ru") }}</div>
              <div>{{ item.date }}</div>
            </div>
          </div>
          <button @click="payFine(item.id)"><span>Pay</span></button>
        </div>
      </div>
      <div class="bank-main_fines-desc" v-else>{{ loc("bank:menu:94") }}</div>
    </div>
  </div>

  <!-- <div class="bank-main-fines bank-main-modal">
    <div class="bank-main-fines__wrap bank-main-modal__wrap">
      <div 
        class="btn-close"
        @click="$emit('closeModal')"
      ></div>
      <div class="bank-main-modal-title">{{loc('bank:menu:91')}}</div>
      <div class="bank-main-modal-desc">{{loc('bank:menu:92')}}</div>
      <div 
        class="bank-main-fines__list"
        v-if="finesList.length > 0"
      >
        <div
          class="bank-main-fines__list-item"
          v-for="item in finesList"
          :key="item.id"
        >
          <div class="bank-main-fines__list-item-reason">{{loc(item.reason)}}</div>
          <div class="bank-main-fines__list-item__info">
            <div class="bank-main-fines__list-item__info-cost">$ {{item.cost.toLocaleString('ru')}}</div>
            <div class="bank-main-fines__list-item__info-date">{{item.date}}</div>
            <div 
              class="bank-main-fines__list-item__info-btn btn-main"
              @click="payFine(item.id)"
            >{{loc('bank:menu:93')}}</div>
          </div>
        </div>
      </div>
      <div 
        class="bank-main-fines__desc"
        v-else
      >{{loc('bank:menu:94')}}</div>
    </div>
  </div> -->
</template>

<script>
import { mapState, mapGetters } from "vuex";
export default {
  name: "BankMainFines",
  computed: {
    ...mapState("bank", ["finesList"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    payFine(id) {
      window.mp.triggerServer("bank:payPenalty", id);
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

.bank-main_fines {
  .bank-modal_wrap {
    padding: conv(34, true) conv(28, true) conv(35, true);
  }

  .bank-modal_close {
    margin-bottom: conv(46, true);
  }

  .bank-modal_title {
    margin-bottom: conv(30, true);
  }

  .bank-main_fines-list {
    display: flex;
    flex-direction: column;
    width: conv(672);
    max-height: conv(449, true);
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

  .bank-main_fines-item {
    height: conv(116, true);
    min-height: conv(116, true);
    background: rgba(255, 255, 255, 0.04);
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: conv(20, true) conv(23) conv(20, true) conv(46);

    & > div {
      position: relative;
      display: flex;
      flex-direction: column;
      justify-content: space-between;

      &::after {
        content: "";
        position: absolute;
        background: #ffffff;
        /* background: rgba(255, 255, 255, 0.09); */
        box-shadow: 0px 0px conv(14.6364) rgba(255, 255, 255, 0.55);
        transform: rotate(-90deg) translateY(-100%);
        width: conv(27.18, true);
        height: conv(2.09);
        left: conv(-35);
        top: 50%;
      }
    }

    &_reason {
      font-weight: 700;
      font-size: conv(24, true);
      line-height: conv(29, true);
      text-transform: uppercase;
      color: #ffffff;
    }

    &_info {
      display: flex;
      align-items: flex-end;
      justify-content: space-between;

      div {
        font-weight: 700;
        text-transform: uppercase;

        &:first-child {
          font-size: conv(32, true);
          line-height: conv(38, true);
          color: #ffffff;
        }

        &:last-child {
          font-size: conv(20, true);
          line-height: conv(24, true);
          text-align: right;
          color: rgba(255, 255, 255, 0.5);
        }
      }
    }

    & > button {
      cursor: pointer;
      position: relative;
      background: rgba(255, 255, 255, 0.05);
      display: flex;
      align-items: center;
      justify-content: center;
      width: conv(243);
      height: conv(75, true);
      border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
      background: linear-gradient(
        rgba($color: #301934 , $alpha: 0.25),
        rgba($color: #591b87, $alpha: 0.25)
      );
      transition: 0.3s ease;
      box-shadow: inset 0 0 1.389vmin rgba($color: #dc2028, $alpha: 0.86);

      &:hover {
        transition: 0.3s ease;
        box-shadow: inset 0vh 0vh 13.889vh #301934 ;
        filter: drop-shadow(0vh 0vh conv(15) rgba(71, 44, 132, 0.5));
      }

      span {
        z-index: 3;
        font-weight: 700;
        font-size: conv(24, true);
        line-height: conv(29, true);
        text-align: center;
        text-transform: uppercase;
        color: #ffffff;
      }

      
    }

    &:not(:last-child) {
      margin-bottom: conv(10, true);
    }
  }

  .bank-main_fines-desc{
    color: white;
  }
}
/* .bank {
  .bank-main-fines {
    .bank-main-modal-title {
      max-width: 22rem;
    }
    .bank-main-modal-title,
    .bank-main-modal-desc {
      padding-left: 1rem;
    }
    &__wrap.bank-main-modal__wrap {
      width: fit-content;
      padding: 2rem 2rem 0.5rem 2rem;
    }
    &__list {
      max-height: 30rem;
      overflow-y: auto;
      padding: 1rem;
      padding-right: 1.35rem;
      margin-right: -1.35rem;
      &::-webkit-scrollbar {
        width: 0.2rem;
        background-color: transparent;
        &-thumb {
          background: #5e37b0;
          border-radius: 0.3rem;
        }
      }
      &-item {
        width: 22rem;
        min-width: 22rem;
        margin-bottom: 0.7rem;
        background: #ffffff;
        box-shadow: 0 0.3rem 1rem rgba(49, 41, 184, 0.2);
        border-radius: 0.5rem;
        padding: 1rem 1.5rem;
        display: flex;
        flex-direction: column;
        &-reason {
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          color: #200c49;
          margin-bottom: 0.6rem;
        }
        &__info {
          display: flex;
          align-items: center;
          justify-content: space-between;
          &-cost {
            font-weight: bold;
            font-size: 1.5rem;
            letter-spacing: 0.03em;
            color: #ee443a;
          }
          &-date {
            font-size: 1rem;
            line-height: 1.2rem;
            letter-spacing: 0.03em;
            color: rgba(32, 12, 73, 0.4);
            margin-right: 1.5rem;
          }
          &-btn.btn-main {
            margin-right: -0.5rem;
            margin-bottom: -0.35rem;
            height: 2.4rem;
            padding: 0 1.4rem;
            border: 0.1rem solid #5e37b0;
            border-radius: 5rem;
            background-color: transparent;
            font-weight: normal;
            font-size: 1rem;
            color: #5e37b0;
            &:hover {
              background-color: #5e37b0;
              color: #fff;
            }
          }
        }
        &:last-child {
          margin-bottom: 0;
        }
      }
    }
    &__desc {
      padding: 0 0 2rem 1rem;
      font-size: 2rem;
      line-height: 2.2rem;
      letter-spacing: 0.03em;
      color: #200c49;
    }
  }
} */
</style>
