<template>
  <div
    :class="[{ active: dress.id === currentDressId }, 'dress']"
    @click="setCurrentDressId(dress.id)"
  >
    {{ loc(dress.label) }}
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters("localization", ["loc"]),
  },

  props: {
    dress: Object,
    currentDressId: Number,
    setCurrentDressId: Function,
  },
};
</script>

<style lang="scss" scoped>
.dressing-up-menu {
  .dress {
    display: flex;
    align-items: center;
    justify-content: flex-start;
    width: calc(100% - 0.789rem);
    font-weight: 700;
    font-size: 1.263rem;
    line-height: 1.526rem;
    text-transform: uppercase;
    color: #ffffff;
    height: 4.632rem;
    min-height: 4.632rem;
    background: rgba(0, 0, 0, 0.2);
    padding: 0 1.895rem;
    border: 0.053rem solid rgba($color: white, $alpha: 0.09);
    position: relative;
    cursor: pointer;
    transition: 0.3s;

    &:not(:last-child) {
      margin-bottom: 0.526rem;
    }

    &::after {
      content: "";
      position: absolute;
      top: 50%;
      left: 0.105rem;
      transform: translateY(-50%) rotate(-90deg);
      width: 1.431rem;
      height: 0.11rem;
      background: #ffffff;
      border: 0.055rem solid rgba(255, 255, 255, 0.09);
      box-shadow: 0px 0px 0.77rem rgba(255, 255, 255, 0.55);
    }

    /* &:before {
      content: "";
      opacity: 0;
      height: 1px;
      width: 1.5rem;
      position: absolute;
      left: 0.7rem;
      background: linear-gradient(
        90.01deg,
        #1c1c1c 0%,
        #c1e704 38.53%,
        #1c1c1c 85.03%
      );
      transform: rotate(90deg);
    } */

    &.active,
    &:hover {
      background: linear-gradient(
        180deg,
        rgba(71, 44, 132, 0.5) 0%,
        rgba(75, 0, 130, 0.5) 100%
      );
      border: 0.053rem solid rgba($color: white, $alpha: 0.09);
      transition: 0.3s;
      /* &:before {
        opacity: 1;
        transition: 0.3s;
      } */
    }
  }
}
</style>
