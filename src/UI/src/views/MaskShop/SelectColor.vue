<template>
  <div class="select-color">
    <div class="title">
      <span>Color - </span>
      <span class="value">{{ value }}</span>
    </div>
    <div class="list">
      <Card
        v-for="(item, key) of list"
        :key="key"
        :imgSize="0.7"
        :layout="'round'"
        :img="img"
        :isActive="key === value"
        :title="`Color ${item}`"
        @onSelect="() => $emit('input', key)"
      />
    </div>
  </div>
</template>

<script>
import Card from '../UI/components/Card.vue'
export default {
  props: {
    value: Number,
    list: Array,
    img: String,
  },
  components: { Card },
}
</script>

<style lang="scss" scoped>
.select-color {
  .title {
    margin-bottom: 2.5vh;
    color: white;
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 600;
    font-size: 1.66vh;
    line-height: 2.03vh;
    color: #ffffff;
    .value {
      background: linear-gradient(
        180deg,
        #301934  0%,
        #ea0505 49.48%,
        #301934  100%
      );
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
    }
  }

  .list {
    width: 21.833vh;
    display: flex;
    max-height: 46.296vh;
    flex-wrap: wrap;
    gap: 3.704vh 2.685vh;
    overflow-y: auto;
    .card {
      width: 8.981vh;
      height: 8.796vh;
    }
    &::-webkit-scrollbar {
      width: 0.28vh;
      height: 0.28vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.23);
    }
    &::-webkit-scrollbar-thumb {
      background: #ff0000;
    }
  }
}
</style>
