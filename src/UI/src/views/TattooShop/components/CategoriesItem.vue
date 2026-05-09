<template>
  <div class="categories-item" @click="clickItem">
    <span class="categories-item__title">{{ loc(item.title) }}</span>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "CategoriesItem",
  methods: {
    clickItem() {
      this.$emit("onItemClick", this.item);
    },
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
  props: {
    item: Object,
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.categories-item {
  width: 100%;
  display: flex;
  justify-content: flex-start;
  align-items: center;
  height: conv(88);
  min-height: conv(88);
  padding-left: conv(50);
  background: rgba(0, 0, 0, 0.2);
  cursor: pointer;
  transition: 0.2s ease;

  &:hover {
    background: rgba(255, 255, 255, 0.05);
  }

  span {
    white-space: nowrap;
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    text-transform: uppercase;
    color: #ffffff;
  }
}
/* .categories-item {
  flex: 0 0 3.25rem;
  width: 15.4rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 0.55rem 0 1.05rem;
  margin: 0.8rem 0 0 0;
  background: rgba(255, 255, 255, 0.09);
  font-weight: 300;
  border: 1px solid transparent;
  &:hover{
    background: rgba(255, 255, 255, 0.4);
  }
  &.active {
    border-color: rgba(193, 231, 4, 0.3);
    .categories-item__title {
      color: rgba(193, 231, 4, 0.65);
    }
  }
  &:first-child {
    margin: 0;
  }
  &__title {
    color: rgba(255, 255, 255, 0.5);
    font-size: 0.8rem;
  }
  &__count {
    color: rgba(255, 255, 255, 0.75);
    font-size: 0.75rem;
  }
} */
</style>
