<template>
  <div class="sidebar">
    <div
      v-for="item in navItems"
      :key="item.key"
      :class="[{ active: item.key == currentPage }, { disabled: !item.enabled }, 'sidebar__item']"
      @click="handleClick(item)"
    >
      <div class="sidebar__item-text">{{ loc(item.text) }}</div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'Header',

  props: {
    setCurrentPage: Function,
    currentPage: String
  },

  data: function() {
    return {
      navItems: [
        {
          text: 'AdminPanel_PlayerSupport',
          key: 'ReportsList',
          enabled: true
        },
        {
          text: 'AdminPanel_PlayerPenalties',
          key: 'PlayerPenalties',
          enabled: true
        },
        {
          text: 'AdminPanel_Organizations',
          key: 'Organizations',
          enabled: true
        },
        {
          text: 'AdminPanel_Businesses',
          key: 'Businesses',
          enabled: true
        },
        {
          text: 'AdminPanel_Commands',
          key: 'Commands',
          enabled: true
        },
        {
          text: 'AdminPanel_Administrators',
          key: 'Administrators',
          enabled: true
        },
        {
          text: 'AdminPanel_Teleports',
          key: 'Teleports',
          enabled: true
        },
        {
          text: 'AdminPanel_Families',
          key: 'Families',
          enabled: true
        },
        {
          text: 'AdminPanel_EventMenu',
          key: 'EventMenu',
          enabled: true
        },
        {
          text: 'AdminPanel_Murders',
          key: 'Murders',
          enabled: true
        },
        {
          text: 'AdminPanel_Arena',
          key: 'Arena',
          enabled: true
        }
      ]
    }
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  },
  methods: {
    handleClick: function (item) {
      if (!item.enabled) return
      this.setCurrentPage(item.key)
    }
  }
}
</script>

<style lang="scss" scoped>
.sidebar{
  width: 9.6vw;
  min-width: 9.5rem;
  max-width: 11rem;
  height: 100%;
  background: rgba(3, 6, 14, 0.9);
  border-right: 1px solid rgba(255, 255, 255, 0.08);
  display: flex;
  flex-flow: column;
  position: relative;
  &:after{
    content: '';
    width: 1px;
    height: 100%;
    position: absolute;
    right: 0;
    top: 0;
    background: linear-gradient(180deg, rgba(255,255,255,0.15) 0%, rgba(255,255,255,0.02) 100%);
  }
  &__item{
    min-height: 8.3%;
    border-bottom: 1px solid rgba(255, 255, 255, 0.08);
    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    cursor: pointer;
    padding: 0 .55rem;
    transition: background .2s ease;
    &.active {
      background: #92A811;
      box-shadow: inset 0 0 0 1px rgba(255, 255, 255, .15);
      .sidebar__item-text {
        color: rgba(255, 255, 255, 0.95);
      }
    }
    &.disabled {
      cursor: default;
    }
    &:not(.active):not(.disabled):hover {
      background: rgba(255, 255, 255, 0.08);
    }
    &-text{
      font-weight: 700;
      font-size: clamp(.82rem, .88vw, 1rem);
      line-height: 1.2;
      color: rgba(255, 255, 255, 0.9);
      white-space: pre-line;
    }
  }
}
</style>
