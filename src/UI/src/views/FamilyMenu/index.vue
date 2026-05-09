<template>
  <div class="family-menu" :style="currentPage === 'EventPage' ? 'padding: 0 0 0;' : ''">
    <transition name="nav-slide">
      <NavMenu v-if="navData.show" :list="navList" />
    </transition>
    <div class="body">
      <transition name="slide-fade">
      <component :is="currentPage" />
      </transition>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'

import EventPage from './EventPage'
import InfoPage from './InfoPage'
import MemberPage from './MemberPage'
import PropertyPage from './PropertyPage'
import BattlePage from './BattlePage'
import ControlPage from './ControlPage'
import RatingPage from './RatingPage'
import HomePage from './HomePage'
import NavMenu from './components/NavMenu.vue'

export default {
  name: 'FamilyMenu',

  components: {
    EventPage,
    InfoPage,
    MemberPage,
    PropertyPage,
    BattlePage,
    ControlPage,
    RatingPage,
    HomePage,
    NavMenu
},

  data: function() {
    return {
      navList: [
        {
          key: 'InfoPage',
          text: 'familyMenu_0'
        },
        {
          key: 'ControlPage',
          text: 'familyMenu_1'
        },
        {
          key: 'MemberPage',
          text: 'familyMenu_3'
        },
        {
          key: 'PropertyPage',
          text: 'familyMenu_4'
        },
        {
          key: 'EventPage',
          text: 'familyMenu_5'
        },
        {
          key: 'BattlePage',
          text: 'familyMenu_6'
        },
      ],
      modalLeaders: false
    }
  },

  methods: {
    ...mapMutations('familyMenu', ['setCurrentPage']),
  },

  mounted(){
    this.setCurrentPage('InfoPage')
  },

  computed: {
    ...mapState('familyMenu', ['currentPage', 'navData']),
    ...mapGetters('localization', ['loc'])
  },
}
</script>

<style lang="scss" scoped>
.family-menu {
  display: flex;
  flex-flow: column;
  align-items: center;
  justify-content: flex-start;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.96);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  // background-image: url('/img/familyMenu/bg.png');
  padding: 0rem 17.222vh 0;
 text-transform: uppercase;
  z-index: 2;
  color: #fff;
  // transition: .6s;
  .nav-slide-enter-active {
    transition: all 0.9s ease;
  }
  .nav-slide-leave-active {
    transition: all 0.9s ease;
  }
  .nav-slide-enter {
    transform: translateY(-29.63vh);
    opacity: 0;
  }
  .nav-slide-leave-to {
    transform: translateY(-29.63vh);
    opacity: 0;
  }

  .slide-fade-enter-active {
    transition: all .3s ease;
  }
  .slide-fade-leave-active {
    opacity: 0;
  }
  .slide-fade-enter, .slide-fade-leave-to {
    transform: translateX(4.444vh);
    opacity: 0;
  }
  &.control{
    background-image: url('/img/familyMenu/bg-control.png');
    transition: .3s;
  }
  &__nav{
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
    width: 100%;
    position: relative;
    z-index: 1;
    &:after{
      content: '';
      width: 100%;
      height: 1px;
      background: rgba(255, 255, 255, 0.3);
      position: absolute;
      bottom: 0;
      left: 0;
    }
    .btn-leave{
      font-size: 1.1rem;
      line-height: 1.55rem;
      padding-bottom: 1rem;
      border-bottom: .1rem solid transparent;
      color: rgba(255, 255, 255, 0.3);
      position: absolute;
      right: 0;
      top: 0;
      &:hover{
        color: rgba(255, 255, 255, 0.6);
      }
    }
  }
  .body{
    width: 100%;
    display: flex;
  }
}
</style>

<style lang="scss" >
.family-menu {
  @keyframes modalShow{
    from{
      opacity: 0;
    }
    to{
      opacity: 1;
    }
  }
  .btn{
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
    transition: .2s;
    &:hover{
      transition: .2s;
    }
  }
  .modal-wrap{
    display: flex;
    align-items: center;
    justify-content: center;
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100vh;
    background: rgba(0, 0, 0, 0.97);
    z-index: 3;
    animation: modalShow .2s;
  }
}
</style>
