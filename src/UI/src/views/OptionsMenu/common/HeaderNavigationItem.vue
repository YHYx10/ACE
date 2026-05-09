<template>
  <div :id="item.description"
    :class="[{ active: item.location === rightTab }, { shop: item.location === 'Shop' }, 'item']"
    @click="setCurrentTab(item.location)">
    <div :id="item.description" class="item__title">{{ loc(item.title) }}</div>
  </div>
</template>

<script>
import { mapMutations, mapGetters } from 'vuex'

export default {
  name: 'HeaderNavigationMenu',

  props: {
    item: Object,
    rightTab: String
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  },

  methods: {
    ...mapMutations('optionsMenu', ['setCurrentTab'])
  }
}
</script>

<style lang="scss" scoped>
.item {
  color: rgba(255, 255, 255, 0.25);
  letter-spacing: 0.03em;
  align-self: center;
  padding-top: 3.4rem;
  margin-top: -3.4rem !important;
  overflow: hidden;
  position: relative;
  cursor: pointer;
  //min-width: 1725px;

  &::after {
    transition: all 0.2s;
    content: "";
    position: absolute;
    transform: translate(-50%, -50%) scale(0);
    top: 0;
    left: 50%;
    height: 0.1rem;
    width: 100%;
    background: #301934 ;
    border: 1px solid rgba(71, 44, 132, 0.09);
    box-shadow: 0px -5px 75px 20px rgba(71, 44, 132, 0.55);
    opacity: 0;
  }

  &#shop::after {
    background: #FFCD4D;

    box-shadow: 0px -5px 75px 20px #FFCD4D;
  }

  &:not(:last-child) {
    margin: 0 2rem 0 0;
  }

  &.active {
    color: #fff;

    &::after {
      transform: translate(-50%, -50%) scale(1);
      opacity: 1;
    }

    &#shop {
      color: #FFCD4D;

    }
  }

  &__title {
    font-size: 1rem;
    line-height: 1;
  }
}

#shop {
  color: #FFCD4D;
}
</style>
