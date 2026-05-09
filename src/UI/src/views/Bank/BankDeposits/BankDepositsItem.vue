<template>
  <div class="bank-deposits_item">
    <div class="bank-deposits_item-img">
      <img :src="`/img/bank/deposits/${item.Image}.png`" alt="" />
      <img :src="`/img/bank/deposits/${item.Image}.png`" alt="" />
    </div>

    <div class="bank-deposits_item-content">
      <div class="bank-deposits_item-info">
        <div>
          Information about the contribution
          <img
            :class="{ active: item.IsRefill }"
            src="/img/bank/deposits/refill.svg"
            alt=""
          />
          <img
            :class="{ active: item.IsWithdraw }"
            src="/img/bank/deposits/withdraw.svg"
            alt=""
          />
        </div>

        <div>{{ loc(item.Title) }}</div>
        <div>{{ loc(item.Description) }}</div>
      </div>

      <div class="bank-deposits_item-bet">
        <div
          v-for="(elem, index) in [
            ['An annual rate', item.AnnualRate + '%'],
            ['Min.Summ', item.MinMoney],
            ['The deposit term', loc(`bank:menu:141@${item.MaxDays}`)],
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

    <button class="bank-deposits_item-btn bank-btn" @click="watchDeposit(item.Id)">
      Read more
    </button>

  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "BankDepositsItem",
  props: {
    item: Object,
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    watchDeposit(id) {
      this.$emit("setModal", id);
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

.bank-deposits_item {
  width: 100%;
  height: 100%;
  background: rgba(255, 255, 255, 0.04);
  position: relative;
  display: flex;
  align-items: center;
  padding-left: conv(47);

  &-img {
    height: conv(130, true);
    width: conv(128, true);
    position: relative;
    margin-right: conv(43);

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

  &-content {
    display: flex;
    flex-direction: column;
    justify-content: center;
  }

  &-info {
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
            height: conv(18, true);
            width: conv(18, true);
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
        max-width: conv(290, true);
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

  &-bet {
    display: flex;
    margin-top: conv(26, true);

    & > div {
      position: relative;
      white-space: nowrap;

      div {
        &:first-child {
          font-weight: 700;
          font-size: conv(14, true);
          line-height: conv(17, true);
          text-transform: uppercase;
          color: #ffffff;
        }

        &:last-child {
          font-weight: 700;
          font-size: conv(28, true);
          line-height: conv(34, true);
          text-transform: uppercase;
          color: #ffffff;
        }
      }

      &:not(:last-child) {
        margin-right: conv(33);
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
  }

  &-btn {
    position: absolute;
    top: 0;
    right: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    height: conv(75, true);
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    width: conv(171);
    font-weight: 700;
    font-size: conv(24, true);
    line-height: conv(29, true);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    cursor: pointer;
  }
}

/* .bank-deposits-item{
  padding: 2.5rem 2rem 4.25rem 2rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  border-radius: 1rem;
  background-color: #030C20;
  background-size: contain;
  background-repeat: no-repeat;
  background-position: right;
  position: relative;
  height: 15rem;
  margin-bottom: 2.5rem;
  &-title{
    font-weight: bold;
    font-size: 2rem;
    line-height: 2rem;
    letter-spacing: 0.03em;
    margin-bottom: .2rem;
  }
  &-desc{
    font-size: 1rem;
    line-height: 1.2rem;
    letter-spacing: 0.03em;
    color: rgba(255, 255, 255, 0.4);
  }
  &__info{
    display: flex;
    align-items: center;
    &-col{
      display: flex;
      flex-direction: column;
      margin-right: 2.7rem;
    }
    &-value{
      font-weight: normal;
      font-size: 1.5rem;
      line-height: 1.8rem;
    }
    &-desc{
      font-weight: normal;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      color: rgba(255, 255, 255, 0.4);
    }
  }
  &__interaction{
    position: absolute;
    bottom: 0;
    transform: translateY(1rem);
    left: 2rem;
    display: flex;
    align-items: flex-start;
    justify-content: flex-start;
    &-btn{
      display: flex;
      align-items: center;
      justify-content: center;
      width: 10rem;
      height: 4rem;
      margin-right: 1rem;
      background: #5E37B0;
      border-radius: .2rem;
      font-size: 1rem;
    }
  }
  &-provision{
    width: 2rem;
    height: 2rem;
    border: .1rem solid rgba(255, 255, 255, 0.3);
    box-sizing: border-box;
    border-radius: .2rem;
    margin-right: 1rem;
    position: relative;
    display: flex;
    align-items: center;
    justify-content: center;
    &:before{
      content: '';
      width: 1.1rem;
      height: 1.1rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      opacity: .3;
    }
    &.active{
      &:before{
        opacity: 1;
      }
    }
    &.replenishment{
      &:before{
        background-image: url('/img/bank/replenishment.png');
      }
    }
    &.completion{
      &:before{
        background-image: url('/img/bank/completion.png');
      }
    }
  }
} */
</style>
