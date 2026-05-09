<template>
  <div class="item">
    <div class="item__info">
      <div class="category">category</div>
      <div class="title">{{ loc(item.title) }}</div>
      <div class="subtitle">{{ loc(item.subtitle) }}</div>
    </div>
    <button class="item__btn">look</button>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'NavigationItem',

  props: {
    item: Object
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  }
}
</script>

<style lang="scss" scoped>
.item {
  width: 16.6rem;
  height: 12.6rem;
  border: 1px solid rgba(255, 255, 255, 0.1);
  padding: 1rem 1.25rem 1.4rem 1.75rem;
  display: flex;
  flex-flow: column nowrap;
  justify-content: space-between;
  background: bottom right / cover no-repeat url("/img/optionsMenu/informationTab/navItemBg.png");

  &:not(:last-child) {
    margin: 0 0 1rem 0;
  }

  &__info {
    .title {
      font-size: 1.77rem;
      line-height: 2.1rem;
    }

    .subtitle {
      font-size: 0.7rem;
      line-height: 0.8rem;
      margin-top: 0;
    }

    .category {
      font-size: 0.66rem;
      line-height: 0.77rem;

      &:before {
        left: -0.88rem;
      }
    }
  }

  &__btn {
    max-width: 8rem;
  }
}
</style>
