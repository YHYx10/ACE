<template>
  <div class="animations-menu">
    <exit-button @click="close" class="exit" />
    <div class="header">
      <title-component
        :titleSecondary="'Animations'"
        :about="'Animation management'"
      />
      <nav-menu
        :list="getCategories()"
        :current="getCurrentCategoryKey()"
        @onSelect="setCurrentCategory"
      />
    </div>
    <div class="display-state">
      {{ currentKey !== null ? 'Catalog' : 'Catalog' }}
    </div>
    <div class="main">
      <animations-list
        :currentAnimations="currentAnimations"
        @onSelect="onSelect"
      />
      <keys-list />
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState, mapMutations } from 'vuex'

import AnimationsList from './components/AnimationsList'
import KeysList from './components/KeysList'
import TitleComponent from '../UI/components/TitleComponent.vue'
import NavMenu from './components/NavMenu.vue'
import ExitButton from '../UI/components/ExitButton.vue'

export default {
  name: 'AnimationsMenu',

  components: {
    AnimationsList,
    KeysList,
    TitleComponent,
    NavMenu,
    ExitButton,
  },

  data() {
    return {
      isModeSetFavorite: false,
    }
  },

  computed: {
    ...mapState('animationsMenu', [
      'categories',
      'currentAnimations',
      'currentKey',
    ]),
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    ...mapMutations('animationsMenu', [
      'setCurrentAnimationsList',
      'setFavorite',
      'saveAnim',
    ]),
    close: function() {
      window.mp.trigger('animations::close')
    },
    getCategories() {
      return this.categories.map(({ name }) => this.loc(name))
    },
    getCurrentCategoryKey() {
      return this.categories.findIndex(
        (v) => v.key === this.currentAnimations.key
      )
    },
    setCurrentCategory(index) {
      console.log('setCurrentCategory')
      this.setCurrentAnimationsList(this.categories[index].key)
    },
    onSelect(item) {
      if (this.currentKey !== null) return this.saveAnim(item)
      else window.mp.trigger('animations::play', item.key)
    },
  },
}
</script>

<style lang="scss" scoped>
.animations-menu {
  width: 100vw;
  height: 100vh;
  position: absolute;
  top: 0;
  left: 0;
  padding: 0 6.296vh;
  font-weight: bold;
  letter-spacing: 0.05em;
  color: #fff;
  display: flex;
  flex-flow: column;
  &::before {
    content: '';
    position: absolute;
    width: 100%;
    height: 100%;
    // background: url('./assets/img/test.png');
    background-repeat: no-repeat;
    background-size: cover;
    left: 0;
    z-index: -1;
    background: linear-gradient(
      270deg,
      rgba(0, 0, 0, 0.85) 79.81%,
      rgba(0, 0, 0, 0.85) 100%
    );
  }
  background: url('./assets/img/bg.png');
  background-size: cover;
  background-repeat: no-repeat;
  .exit {
    position: absolute;
    top: 5.37vh;
    right: 6.389vh;
    white-space: nowrap;
    width: min-content;
  }

  .header {
    display: flex;
    align-items: flex-end;
    gap: 2.5vw;
    width: 100%;
    position: relative;
    .nav-menu {
      // margin-left: auto;
      margin-right: auto;
    }
  }
  .display-state {
    padding: 3.519vh 0;
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 600;
    font-size: 2.222vh;
    line-height: 2.222vh;
    color: #ffffff;
  }

  .main {
    display: flex;
    gap: 2.778vh;
    justify-content: space-between;
  }
}
</style>
