<template>
  <div class="organization-block">
    <div class="title">{{ title }}</div>
    <div class="desc">{{ desc }}.</div>
    <div class="nav-list">
      <div
        :class="[{ active: item.key === currentChoice }, 'nav-list__item']"
        v-for="item in choiceList"
        :key="item.id"
        @click="setCurrentChoice(item.key)"
      >
        {{ loc(item.text) }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'OrganizationBlock',
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  props: {
    title: String,
    desc: String,
    setCurrentChoice: Function,
    choiceList: Array,
    currentChoice: String,
  },
}
</script>

<style lang="scss" scoped>
div,
button {
  font-family: 'Akrobat';
}
.organization-block {
  display: flex;
  flex-flow: column;
  width: 32.593vh;
  color: #ffffff;
  text-transform: uppercase;
  .title {
    margin-bottom: 0.741vh;
    font-weight: 700;
    font-size: 5.926vh;
    line-height: 7.407vh;
  }
  .desc {
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.315vh;
    color: rgba(255, 255, 255, 0.44);
    margin-bottom: 2.222vh;
  }
  .nav-list {
    display: flex;
    flex-flow: column;
    width: 100%;
    gap: 0.926vh;
    &__item {
      display: flex;
      align-items: center;
      justify-content: flex-start;
      padding: 0 1.778vh;
      font-weight: 300;
      color: rgb(255, 255, 255);
      transition: 0.2s;
      width: 32.407vh;
      height: 8.148vh;
      border: 0.093vh solid transparent;
      border-right: none;
      font-style: normal;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.778vh;
      border-image: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.2) 0%,
          rgba(255, 255, 255, 0) 100%
        )
        1 1 round;
      &::before {
        content: '';
        height: 2.517vh;
        width: 0.194vh;
        background: #ffffff;
        margin-right: 1.852vh;
        box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
      }
      &:hover,
      &.active {
        background: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.15) 0%,
          rgba(255, 255, 255, 0) 100%
        );
        transition: 0.2s;
      }
    }
  }
}
</style>
