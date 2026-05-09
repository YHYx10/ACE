<template>
  <div class="farm-house_sell">
    <div class="farm-house_sell-title"> Sale of resources</div>
    <div class="farm-house_sell-wrap">
      <SellItem v-for="(elem, index) in sellList" :key="index" :elem="elem" />
    </div>
  </div>
</template>

<script>
import SellItem from "./SellItem.vue";
import { mapState } from "vuex";

export default {
  components: { SellItem },
  computed: {
    ...mapState("farmHouse", ["sellList"]),
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house_sell {
  width: 100%;
  height: 100%;
  padding: conv(28) conv(20) conv(39) conv(35);
  display: flex;
  flex-direction: column;
  background: linear-gradient(
    252.44deg,
    rgba(19, 20, 21, 0.99) 0%,
    rgba(31, 34, 37, 0.99) 100%
  );

  &-title {
    font-weight: 700;
    font-size: conv(32);
    line-height: conv(38);
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: conv(25);
  }

  &-wrap {
    display: flex;
    flex-direction: column;
    width: 100%;
    max-height: conv(612);
    overflow-y: auto;

    &::-webkit-scrollbar {
      background: rgba(255, 255, 255, 0.05);
      width: conv(5);

      &-thumb {
        background: #301934 ;
      }
    }
  }
}
</style>