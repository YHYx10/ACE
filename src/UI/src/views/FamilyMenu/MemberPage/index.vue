<template>
  <div class="member-page">
    <div class="member-page__nav">
      <div
        :class="[{ active: item.key === currentTab }, 'item']"
        v-for="item in navList"
        :key="item.id"
        @click="setCurrentTab(item.key)"
      >
        {{ loc(item.text) }}
      </div>
    </div>
    <component :is="currentTab" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import AccessTab from './AccessPage'
import RatingTab from './RatingPage'

export default {
  name: 'MemberPage',

  components: {
    AccessTab,
    RatingTab,
  },

  data: function() {
    return {
      currentTab: '',
      navList: [
        {
          text: 'fam:menu:member:1',
          key: 'AccessTab',
        },
        {
          text: 'fam:menu:member:2',
          key: 'RatingTab',
        },
      ],
    }
  },

  methods: {
    setCurrentTab: function(value) {
      this.currentTab = value
    },
  },

  computed: {
    ...mapGetters('localization', ['loc']),
  },

  mounted() {
    this.currentTab = this.navList[0].key
  },
}
</script>

<style lang="scss" scoped>
.member-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  width: 100%;
  &__nav {
    margin-top: 6.481vh;
    display: flex;
    align-items: center;
    gap: 1.852vh;
    width: 141.667vh;
    .item {
      display: flex;
      justify-content: center;
      align-items: center;
      transition: 0.2s;
      width: 24.815vh;
      height: 7.407vh;
      border: 0.093vh solid rgba(255, 255, 255, 0.09);
      font-family: 'Akrobat';
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
      text-transform: uppercase;

      color: #ffffff;

      &:hover {
        border: 0.093vh solid rgba(255, 255, 255, 0.15);
      }
      &.active {
        background: rgba(255, 255, 255, 0.09);
        border: 0.093vh solid rgba(255, 255, 255, 0.15);
      }
      &:last-child {
        margin-right: 0;
      }
    }
  }
}
</style>
