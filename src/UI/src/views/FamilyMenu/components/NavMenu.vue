<template>
  <div class="nav-menu">
    <div
      class="item"
      v-for="(item, key) of list"
      :key="key"
      :class="{ active: item.key === currentPage }"
      @click="setCurrentPage(item.key)"
    >
      <div class="title">
        {{ loc(item.text) }}
      </div>
    </div>
  </div>
</template>
<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
export default {
  props: {
    list: Array,
    current: Number,
  },
  computed: {
    ...mapState('familyMenu', ['currentPage']),
    ...mapGetters('localization', ['loc'])
  },
  methods: {
    ...mapMutations('familyMenu', ['setCurrentPage']),
  },
}
</script>

<style lang="scss" scoped>
.nav-menu {
  text-transform: none;
  display: flex;
  align-items: flex-start;
  gap: 0.1875vw;
  font-family: 'Akrobat';
  font-weight: 700;
  font-size: 2.222vh;
  line-height: 2.22vh;
  text-transform: uppercase;
  color: rgba(255, 255, 255, 0.24);
  position: relative;
    z-index: 1;

  .item {
    position: relative;
    padding: 6.074vh 1.5vw 1.435vh 1.5vw;
    display: flex;
    align-items: center;
    transition: 0.3s;
    &:hover {
      color: rgba(255, 255, 255, 0.5);
      transition: 0.1s;
      .title {
        color: rgba(255, 255, 255, 0.5);
        text-shadow: 0vh 0vh 1.67vh rgba(255, 255, 255, 0.5);
      }
    }
    &.active {
      &::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        box-sizing: content-box;
        width: 100%;
        height: 0.37vh;
        background: #ff0000;
      }
      &::after {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        height: 9.259vh;
        width: 100%;
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.06) 0%,
          rgba(71, 44, 132, 0) 100%
        );
      }
      .title {
        color: #ffffff;
        text-shadow: 0vh 0vh 3.33vh rgba($color: #ffffff, $alpha: 0.8);
      }
    }
  }
}
</style>
