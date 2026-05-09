<template>
  <div class="report-menu">
    <div class="report-menu__viewport">
      <div class="report-menu__body">
        <div class="report-menu__close" @click="closeMenu"></div>
        <Header :setCurrentPage="setCurrentPage" :currentPage="currentPage" />
        <div class="report-menu__content">
          <component :is="currentPage"/>
        </div>
        <StaffChat />
      </div>
    </div>
  </div>
</template>

<script>
import Header from './components/Header'
import StaffChat from './components/StaffChat'
import ReportsList from './ReportsList'
import PlayerPenalties from './PlayerPenalties'
import Organizations from './Organizations'
import Businesses from './Businesses'
import Administrators from './Administrators'
import Teleports from './Teleports'
import Families from './Families'
import Commands from './Commands'
import EventMenu from './EventMenu'
import Murders from './Murders'
import Arena from './Arena'
import { mapMutations, mapState } from 'vuex'
export default {
  name: 'ReportMenu',
  components: {
    Header,
    StaffChat,
    ReportsList,
    PlayerPenalties,
    Organizations,
    Businesses,
    Administrators,
    Teleports,
    Families,
    Commands,
    EventMenu,
    Murders,
    Arena
  },
  computed: {
    ...mapState('reportMenu', ['currentPage'])
  },
  methods: {
    ...mapMutations('reportMenu', ['refreshCurrentChat', 'setCurrentPage']),
    closeMenu: function () {
      window.mp.trigger('report:closemenu')
    }
  }
}
</script>

<style lang="scss" scoped>
.report-menu{
  width: 100%;
  height: 100%;
  position: absolute;
  top: 0;
  left: 0;
  background: rgba(2, 5, 12, 0.72);
  backdrop-filter: blur(2px);
  z-index: 3;
  overflow: hidden;
  &:before {
    content: '';
    position: absolute;
    inset: 0;
    background: radial-gradient(circle at 42% 42%, rgba(255, 255, 255, 0.09), rgba(0, 0, 0, 0.55) 63%);
    pointer-events: none;
  }
  &__viewport {
    width: 100%;
    height: 100%;
    position: relative;
  }
  &__close {
    width: 2rem;
    height: 2rem;
    position: absolute;
    right: 1rem;
    top: 1rem;
    border-radius: .45rem;
    background: rgba(228, 72, 72, 0.9);
    z-index: 3;
    cursor: pointer;
    &:before,
    &:after {
      content: '';
      position: absolute;
      top: 50%;
      left: 50%;
      width: 1.2rem;
      height: .2rem;
      background: #fff;
    }
    &:before {
      transform: translate(-50%, -50%) rotate(45deg);
    }
    &:after {
      transform: translate(-50%, -50%) rotate(-45deg);
    }
  }
  &__body{
    display: flex;
    height: 100%;
    width: 100%;
    color: #fff;
    text-transform: uppercase;
    position: absolute;
    top: 0;
    left: 0;
  }
  &__content {
    flex: 1;
    min-width: 0;
    border-left: 1px solid rgba(255, 255, 255, 0.13);
    border-right: 1px solid rgba(255, 255, 255, 0.13);
    background: rgba(2, 7, 17, 0.22);
    box-shadow: inset 0 0 2.6rem rgba(180, 205, 255, 0.055);
    overflow: hidden;
  }
}
</style>

<style lang="scss">
.report-menu{
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
  .scroll{
    &::-webkit-scrollbar{
      background: transparent;
      width: .3rem;
    }
    &::-webkit-scrollbar-thumb{
      background: #4C4F56;
      border-radius: 1.1rem;
    }
  }
  button {
    transition: background .16s ease, border-color .16s ease, box-shadow .16s ease, transform .16s ease;
    &:hover {
      border-color: rgba(215, 255, 63, 0.55);
      box-shadow: 0 0 .95rem rgba(215, 255, 63, 0.12);
      transform: translateY(-1px);
    }
  }
}
</style>
