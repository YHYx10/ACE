<template>
  <div class="farm-house_shop-item">
    <div class="farm-house_shop-item_header">
      <div v-if="item.param">{{ loc(item.param) }}</div>
      <div v-if="item.minTime">{{ 3 }}&nbsp;pcs</div>
    </div>
    <img :src="img" alt="" />
    <div class="farm-house_shop-item_footer">
      <div class="title">{{ loc(item.title) }}</div>
      <div>${{ item.cost.toLocaleString("ru-RU") }}</div>
      <button @click="() => $emit('buyProduct', item.id)">Buy</button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  props: {
    item: Object,
    img: String,
    time: String,
    param: String,
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>