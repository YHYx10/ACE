<template>
  <div class="farm-house_shop">
    <div class="farm-house_shop-title">Fertilizer</div>
    <div class="farm-house_shop-list">
      <ShopItem
        v-for="item in seedsList"
        :key="item.id"
        :item="{...item, cost: item.cost* 3}"
        @buyProduct="(id) => buyProduct(id)"
        :img="`/img/farmHouse/seeds/${item.img}.png`"
      />
      <ShopItem
        v-for="item in needfulList"
        :key="item.id"
        :item="item"
        @buyProduct="(id) => buyProduct(id)"
        :img="`/img/farmHouse/${item.img}.png`"
      />
      <ShopItem
        v-for="item in fertilizersList"
        :key="item.id"
        :item="{...item, cost: item.cost* 3}"
        @buyProduct="(id) => buyProduct(id)"
        :img="`/img/farmHouse/fertilizers/${item.img}.png`"
      />
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
import ShopItem from "./ShopItem.vue";

export default {
  components: { ShopItem },
  computed: {
    ...mapState("farmHouse", ["seedsList", "fertilizersList", "needfulList"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    buyProduct: function (id) {
      window.mp.triggerServer("farmHouse:buyProduct", id);
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house_shop {
  width: 100%;
  height: 100%;
  background: linear-gradient(
    252.44deg,
    rgba(19, 20, 21, 0.99) 0%,
    rgba(31, 34, 37, 0.99) 100%
  );
  display: flex;
  flex-direction: column;
  padding: conv(28) conv(62) conv(28) conv(77);

  &-title {
    font-weight: 700;
    font-size: conv(32);
    line-height: conv(38);
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: conv(26);
  }

  &-list {
    width: 100%;
    overflow-y: auto;
    max-height: conv(622);
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: conv(10);
    padding-right: conv(10);

    &::-webkit-scrollbar {
      background: rgba(255, 255, 255, 0.05);
      width: conv(5);

      &-thumb {
        background: #301934 ;
      }
    }
  }

  &-item {
    width: 100%;
    max-height: conv(306);
    min-height: conv(306);
    background: rgba(255, 255, 255, 0.03);
    padding: conv(11) conv(20) conv(13);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    position: relative;
    cursor: pointer;

    &:hover {
      .farm-house_shop-item_footer {
        div {
          opacity: 0;
        }

        button {
          opacity: 1;
        }
      }
    }

    &_header {
      display: flex;
      align-items: center;
      justify-content: space-between;

      div {
        &:first-child {
          font-weight: 500;
          font-size: conv(18);
          line-height: conv(20);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }

        &:nth-child(2) {
          height: conv(34);
          background: rgba(255, 255, 255, 0.04);
          border-radius: conv(25);
          padding: 0 conv(25);
          display: flex;
          align-items: center;
          justify-content: center;
          font-weight: 700;
          font-size: conv(14);
          line-height: conv(17);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }

    & > img {
      position: absolute;
      bottom: conv(117);
      left: 50%;
      transform: translateX(-50%);
      width: conv(146);
      height: auto;
    }

    &_footer {
      display: flex;
      align-items: center;
      flex-direction: column;
      width: 100%;

      div {
        &:first-child {
          font-weight: 700;
          font-size: conv(24);
          line-height: conv(29);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
          margin-bottom: conv(3);
        }

        &:nth-child(2) {
          font-weight: 700;
          font-size: conv(36);
          line-height: conv(43);
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }
        opacity: 1;
        transition: 0.2s ease;
      }

      button {
        opacity: 0;
        transition: 0.2s ease;
        cursor: pointer;
        display: flex;
        justify-content: center;
        align-items: center;
        height: conv(75, true);
        width: 100%;
        position: absolute;
        bottom: 0;
        left: 50%;
        transform: translateX(-50%);
        //background: linear-gradient(180deg, #301934  0%, #591b87 100%);
        font-style: normal;
        font-weight: 700;
        font-size: conv(24, true);
        line-height: conv(29, true);
        text-align: center;
        text-transform: uppercase;
        color: #ffffff;
        z-index: 5;
        opacity: 0;
        border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
        background: linear-gradient(
          rgba($color: #301934 , $alpha: 0.25),
          rgba($color: #591b87, $alpha: 0.25)
        );
        transition: 0.3s ease;
        box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

        &:hover {
          transition: 0.3s ease;
          box-shadow: inset 0px 0px 7.5rem #301934 ;
          filter: drop-shadow(0px 0px conv(15) rgba(71, 44, 132, 0.5));
        }
      }
    }
  }
}
</style>