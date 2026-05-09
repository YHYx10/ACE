<template>
  <div class="farm-house_sell-item" :class="{ empty: elem.count === 0 }">
    <div>
      <div class="farm-house_sell-item_price">
        <div>Cost per pcs</div>
        <div>{{ elem.price }}</div>
      </div>
      <div class="farm-house_sell-item_img">
        <img :src="`/img/farmHouse/sell/${elem.img}.png`" alt="" />
      </div>
      <div class="farm-house_sell-item_count">
        <div>{{ elem.name }}</div>
        <div>{{ elem.count }}&nbsp;pcs</div>
      </div>
    </div>
    <div>
      <div class="farm-house_sell-item_input">
        <input
          type="number"
          v-model="currentNum"
          placeholder="Enter the quantity.."
        />
        <button
          @click="sell"
          v-if="elem.count !== 0"
          :class="{ active: currentNum > 0 && currentNum <= elem.count }"
          class="farn-house_sell-item_btn bank-btn"
        >
        Sell
        </button>
      </div>
      <div class="farm-house_sell-item_money">
        ${{
          currentNum > 0 && elem.count >= currentNum
            ? (currentNum * elem.price).toLocaleString("ru-RU")
            : 0
        }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    elem: Object,
  },
  data() {
    return {
      currentNum: "",
    };
  },
  methods: {
    sell() {
      window.mp.triggerServer("farmHouse:sell", this.elem.name, this.currentNum);
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house_sell-item {
  width: 100%;
  padding-left: conv(36);
  background: rgba(255, 255, 255, 0.01);
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: conv(120);
  min-height: conv(120);

  & > div {
    display: flex;
    align-items: center;
    height: 100%;
  }

  &:not(:last-child) {
    margin-bottom: conv(3);
  }

  &_price {
    margin-right: conv(33);
    display: flex;
    flex-direction: column;
    align-items: flex-end;

    div {
      &:first-child {
        font-weight: 500;
        font-size: conv(16);
        line-height: conv(19);
        text-transform: uppercase;
        color: #ffffff;
        margin-bottom: conv(2);
      }

      &:last-child {
        font-weight: 700;
        font-size: conv(24);
        line-height: conv(29);
        text-transform: uppercase;
        color: #ffffff;
      }
    }
  }

  &_img {
    height: 100%;
    width: conv(120);
    min-width: conv(120);
    position: relative;
    display: flex;
    align-items: center;
    justify-content: center;
    background: rgba(255, 255, 255, 0.03);
    margin-right: conv(30);

    img {
      height: 70%;
    }
  }

  &_count {
    display: flex;
    flex-direction: column;

    div {
      &:first-child {
        font-weight: 700;
        font-size: conv(24);
        line-height: conv(29);
        text-transform: uppercase;
        color: #ffffff;
        margin-bottom: conv(6);
      }

      &:last-child {
        font-weight: 700;
        font-size: conv(20);
        line-height: conv(24);
        text-transform: uppercase;
        color: #ffffff;
      }
    }
  }

  &_input {
    width: conv(425);
    display: flex;
    align-items: center;
    height: conv(75);

    input {
      height: 100%;
      width: conv(253);
      background: rgba(0, 0, 0, 0.25);
      padding: 0 conv(20);
      color: #ffffff;
      border: none;
      outline: none;
      font-size: conv(24);
      line-height: conv(29);

      &::placeholder,
      & {
        font-weight: 700;
        text-transform: uppercase;
      }

      &::placeholder {
        color: rgba(255, 255, 255, 0.5);
        font-size: conv(16);
        line-height: conv(19.2);
      }

      &::-webkit-outer-spin-button,
      &::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
      }
    }

    button {
      height: 100%;
      width: conv(199);
      display: flex;
      align-items: center;
      justify-content: center;
      background: linear-gradient(
        180deg,
        #301934  0%,
        #591b87 100%,
        #591b87 100%
      );
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      cursor: pointer;
    }
  }

  &_money {
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    width: conv(213);
    font-weight: 700;
    font-size: conv(32);
    line-height: conv(38);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
  }

  &.empty {
    .farm-house_sell-item_input {
      pointer-events: none;
      opacity: 0.2;
    }

    .farm-house_sell-item_money {
      opacity: 0.2;
    }
  }

  .farm-house_sell-item_input button {
    &:not(.active) {
      pointer-events: none;
      opacity: 0.2;
    }
  }
}
</style>