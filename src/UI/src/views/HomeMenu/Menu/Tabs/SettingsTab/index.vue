<template>
  <div class="settings-tab">
    <div class="category">section</div>
    <div class="title">{{ loc('mm_setts_tit') }}</div>
    <div class="settings-tab__content">
      <div class="settings-tab-left">
        <div class="settings-tab-left-buttons">
          <div class="settings-tab-left-button"
               v-for="(btn, index) in buttons"
               :key="index"
               :class="{'settings-tab-left-button-selected': index == current}"
               @click="current = index"
          >
            <div class="category">category</div>
            {{ loc(btn.name) }}
          </div>
        </div>
      </div>
      <component :is="buttons[current].tab1" class="settings-tab-center"/>
      <component :is="buttons[current].tab2" class="settings-tab-right"/>
    </div>
  </div>
</template>

<script>
import Interface from './tabs/Interface'
import Controls from './tabs/Controls'
import Chat from './tabs/Chat'
import Account from './tabs/Account'
import AutoLogin from './tabs/AutoLogin'
import {mapGetters} from 'vuex'

export default {
  name: 'SettingsTab',
  data() {
    return {
      current: 0,
      buttons: [
        {name: "mm_setts_m_t_1", tab1: "Interface"},
        {name: "mm_setts_m_t_2", tab1: "Controls"},
        {name: "mm_setts_m_t_3", tab1: "Chat"},
        {name: "mm_setts_m_t_4", tab1: "Account", tab2: AutoLogin}
      ]
    }
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  },
  components: {
    Interface,
    Controls,
    Chat,
    Account,
    AutoLogin,
  }
}
</script>

<style lang="scss">
.settings-tab {
  padding: 2.5rem 15rem 0;
  color: #fff;

  .billet-item .title {
    letter-spacing: 0.03rem !important;
    font-size: 1.05rem !important;
    line-height: 1 !important;
  }

  &__content {
    position: relative;
    display: flex;
    margin-top: 1rem;
  }

  &-left {
    min-width: 18rem;
    max-width: 18rem;

    &-button {
      padding: 1.22rem 1.5rem;
      border: 1px solid rgba(255, 255, 255, 0.1);
      color: rgba(255, 255, 255, 0.5);
      font-size: 1.33rem;
      line-height: 1.6rem;
      font-weight: 600;
      letter-spacing: 0.05em;
      cursor: pointer;

      .category {
        font-weight: 500;
        font-size: 0.66rem;
        line-height: 0.77rem;
        margin-bottom: 0.05rem;

        &:before {
          left: -0.75rem;
        }
      }

      &:not(&:last-child) {
        margin-bottom: 0.5rem;
      }

      &-selected {
        color: #fff;
        background: linear-gradient(90deg, rgba(255, 255, 255, 0) 0%, rgba(255, 255, 255, 0.07) 40%, rgba(12, 16, 10, 0) 100%);
      }
    }
  }

  &-center {
    position: relative;
    flex: 1 1 100%;
    position: relative;
    margin: 0 1.33rem;
    height: 37.3rem;

    &:not(&.settings-account) {
      overflow-y: scroll;
      padding-right: 2rem;
      scrollbar-width: thin;
      scrollbar-color: #5cff80 #444444;
      &::-webkit-scrollbar {
        display: block;
        width: 0.11rem;
        height: 0;
      }
      &::-webkit-scrollbar-track {
        background: #444444;
      }
      &::-webkit-scrollbar-thumb {
        background-color: #5cff80;
      }
    }
  }

  &-right {
    position: absolute;
    right: -10rem;
    top: 0;
    max-width: 15rem;
  }
}
</style>
