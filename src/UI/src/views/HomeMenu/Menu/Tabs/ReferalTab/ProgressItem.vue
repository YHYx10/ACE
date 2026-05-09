<template>
  <div class="referals-tab-item">
    <div class="referals-tab-item-count">
      <img src="/img/optionsMenu/referalsTab/referals.svg">
      {{ count }}
    </div>
    <div :class="[{'compleeted': compleeted}, 'referals-tab-item-info billet-item']">
      <div class="referals-tab-item-info-icon">
        <img :src="`/img/optionsMenu/programTab/${compleeted ? 'compleeted' : item.Image}.png`">
      </div>
      <div class="referals-tab-item-info-heading">
        <div :class="item.Value ? 'category' : 'title'">{{ loc(item.Desc) }}</div>
        <div v-if="item.Value" class="title">{{ item.Value }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  props: ["count", "item", "compleeted"],
  computed: {
    ...mapGetters("localization", ["loc"])
  }
}
</script>

<style lang="scss" scoped>
.referals-tab-item {
  display: flex;

  &:not(&:first-child) {
    margin-top: 0.5rem;
  }

  &-count {
    display: flex;
    flex-flow: column nowrap;
    justify-content: center;
    align-items: center;
    margin-right: 0.5rem;
    min-width: 5rem;
    max-width: 5rem;
    height: 5rem;
    border: 1px solid rgba(255, 255, 255, 0.1);
    font-size: 1.1rem;
    line-height: 1.2;
    font-weight: 700;
    color: #fff;
    overflow: hidden;

    & img {
      filter: invert(20%) sepia(23%) saturate(5077%) hue-rotate(87deg) brightness(92%) contrast(88%) drop-shadow(0 0 1.5rem #00FF38);
    }
  }

  &-info {
    justify-content: start;
    width: 100%;
    height: 5rem;
    transition: 0.2s ease;

    &:hover:before {
      left: 0;
      height: 100%;
    }
    &.compleeted {
      background: radial-gradient(at 50% bottom, rgba(#33FF60, 0.1) 20%, rgba(0, 0, 0, 0) 80%) !important;
      .title {
        color: #33FF60;
      }
    }

    &-icon {
      width: 3rem;
      height: 3rem;
      filter: drop-shadow(0 0 0.6rem rgba(#00FF38, 0.8));

      & img {
        width: 100%;
        height: 100%;
        object-fit: contain;
      }
    }

    &-heading {
      margin: 0 auto 0 2rem;

      .category:before {
        display: none;
      }

      .title {
        font-weight: 600;
        font-size: 1.3rem;
        line-height: 1.3;
      }
    }
  }
}
</style>