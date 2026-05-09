<template>
  <div class="options-menu">
    <div class="options-menu-body">
      <div class="options-menu__header">
        <div class="heading">
          <div class="title">Charakter Speisekarte</div>
          <div class="desc">Eine kurze Beschreibung dieser Schnittstelle, warum und warum.</div>
        </div>
        <div class="navigation">
          <header-navigation-item v-for="(item, index) in navItems" v-show="showNavElement(item.location)" :key="index"
            :item="item" :rightTab="rightTab" />
          <div class="balance">
            <div class="balance__icon">
              <img src="/img/optionsMenu/statisticsTab/icons/balance.svg">
            </div>
            <div class="balance__info">
              <div class="desc">
                Gleichgewicht (<span>Primarz Coins</span>)
              </div>
              <div class="amount">
                14 881 488
              </div>
            </div>
            <button class="balance__deposit-icon"></button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import HeaderNavigationItem from './common/HeaderNavigationItem'
/*import StatisticsTab from './Tabs/StatisticsTab'
import SettingsTab from './Tabs/SettingsTab'
import OrganizationTab from './Tabs/OrganizationTab'
import ReportsTab from './Tabs/ReportsTab'
import InformationTab from './Tabs/InformationTab'
import ProgramTab from './Tabs/ProgramTab'
import ReferalTab from './Tabs/ReferalTab'

import CloseButton from './common/CloseButton'
import Dialog from './common/OptionDialog'
import CaptAttack from './common/CaptAttack'*/

export default {
  name: 'OptionsMenu',

  components: {
    HeaderNavigationItem,
    /*StatisticsTab,
    SettingsTab,
    OrganizationTab,
    ReportsTab,
    InformationTab,
    ProgramTab,
    
    CloseButton,
    Dialog,
    CaptAttack,
    ReferalTab*/
  },

  data: function () {
    return {
      navItems: [
        { title: 'mm_main_nav_t_1', description: 'mm_main_nav_d_1', location: 'StatisticsTab' },
        { title: 'mm_main_nav_t_2', description: 'mm_main_nav_d_2', location: 'OrganizationTab' },
        { title: 'mm_main_nav_t_3', description: 'mm_main_nav_d_3', location: 'InformationTab' },
        { title: 'mm_main_nav_t_5', description: 'mm_main_nav_d_5', location: 'ProgramTab' },
        { title: 'mm_main_nav_d_6', description: 'mm_main_nav_t_6', location: 'ReferalTab' },
        { title: 'mm_main_nav_t_4', description: 'mm_main_nav_d_4', location: 'SettingsTab' },
        { title: 'optmenu:tabs:tit', description: 'optmenu:tabs:desc', location: 'ReportsTab' },
        { title: 'store', description: 'shop', location: 'ShopTab' },

      ]
    }
  },

  computed: {
    ...mapState('optionsMenu', ['currentTab', 'statistics', 'balance', 'fraction', 'captAttack', 'dialog', 'bp']),
    ...mapState('newDonateShop', ['coins']),
    ...mapGetters('localization', ['loc']),
    rightTab: function () {
      return this.currentTab ? this.currentTab : 'StatisticsTab'
    }
  },
  methods: {
    donate: function () {
      window.mp.trigger("mmenu:open:donate");
    },
    showNavElement() {
      return true;
    },
    exit: function () {
      window.mp.trigger("cef:mmenu:close")
    }
  }
}
</script>

<style lang="scss">
.options-menu {
  display: flex;
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: url("/img/optionsMenu/bg.png"), rgba(0, 0, 0, 0.94);
  background-blend-mode: overlay;
  text-transform: uppercase;
  padding: 2rem 5.5rem 0 5.5em;
  overflow: hidden;
  z-index: 10;

  &-body {
    height: 100%;
    margin: auto;
  }

  &__header {
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;

    .heading {
      white-space: nowrap;
      margin-right: 2rem;

      .title {
        font-size: 1.8rem;
        line-height: 2.3rem;
        font-weight: 900;
        letter-spacing: 0.1em;
        background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        text-fill-color: transparent;
        text-shadow: 0px 0px 112.591px rgba(255, 255, 255, 0.25);
      }

      .desc {
        letter-spacing: 0.03rem;
        color: #fff;
        font-size: 0.6rem;
        line-height: 0.7rem;
      }
    }

    .navigation {
      font-weight: 600;
      display: flex;
      flex: 1 1 100%;
      justify-content: flex-end;

      .shop {
        color: #FFCD4D;

        &::after {
          background: #FFCD4D;
          border: 1px solid rgba(255, 205, 77, 0.09);
          box-shadow: 0px -5px 75px 20px rgba(255, 205, 77, 0.55);
        }

        &.active {
          color: #FFCD4D;
        }
      }

      .balance {
        white-space: nowrap;
        display: flex;
        background: rgba(255, 255, 255, 0.05);
        font-weight: 600;
        border-radius: 2.4rem;
        justify-content: space-between;
        align-items: center;
        padding: 0.7rem 1rem 0.7rem 0.7rem;

        &__icon {
          width: 2.5rem;
          height: 2.5rem;
          border-radius: 50%;
          overflow: hidden;
          transition: 0.5s ease;

          & img {
            width: 100%;
            height: 100%;
            object-fit: contain;
          }
        }

        &__info {
          margin: 0 0.77rem;

          .desc {
            font-size: 0.77rem;
            line-height: 0.94rem;
            color: rgba(255, 255, 255, 0.25);
          }

          .amount {
            font-size: 1.1rem;
            line-height: 1.33rem;
            color: #5CFF80;
          }
        }

        &__deposit-icon {
          position: relative;
          min-width: 1.5rem;
          min-height: 1.5rem;
          max-width: 1.5rem;
          max-height: 1.5rem;
          font-weight: 900;
          color: rgba(0, 0, 0, 0.55);
          background: #FF9A24;
          font-size: 1.2rem;
          line-height: 0;
          box-shadow: 0px 0px 10px 2px rgba(255, 201, 77, 0.55);
          border-radius: 50%;
          overflow: hidden;
          cursor: pointer;
          transition: box-shadow 0.3s ease;

          &:before {
            content: '+';
            line-height: 0;
            margin: auto;
          }

          &:hover {
            transform: scale(1.04);
            box-shadow: 0px 0px 15px 4px rgba(255, 201, 77, 0.55);
          }
        }
      }
    }

    &-capt-team {
      color: #fff;
    }
  }

  &__main {
    position: relative;
    height: 100%;
  }


  /* general */
  .title {
    font-weight: 700;
    font-size: 2.4rem;
    line-height: 2.9rem;
    letter-spacing: 0.05em;
    color: #fff;
  }

  .subtitle {
    display: flex;
    color: rgba(255, 255, 255, 0.5);
    font-size: 0.8rem;
    line-height: 0.9rem;
    letter-spacing: 0.05em;
    margin-top: 0.5rem;
  }

  .category {
    position: relative;
    font-size: 0.65rem;
    line-height: 0.8rem;
    letter-spacing: 0.06em;
    color: rgba(255, 255, 255, 0.5);

    &:before {
      content: '';
      position: absolute;
      height: 1.44rem;
      top: 0;
      left: -0.7rem;
      color: #fff;
      background: #fff;
      border: 1px solid #fff;
      box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
    }
  }

  .billet-item {
    position: relative;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 1rem 0rem 1rem 2rem;
    letter-spacing: 0.03em;
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 50%, rgba(0, 0, 0, 0) 100%) 1;
    background: linear-gradient(90deg, rgba(255, 255, 255, 0) 10%, rgba(255, 255, 255, 0.03) 35%, rgba(12, 16, 10, 0) 100%);

    .title {
      font-weight: 600;
    }

    &:before {
      content: '';
      position: absolute;
      height: 1.44rem;
      top: 50%;
      left: 0.7rem;
      transform: translateY(-50%);
      color: #fff;
      background: #fff;
      border: 1px solid #fff;
      box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
      transition: 0.4s ease;
    }

    &:not(&:last-child) {
      margin-bottom: 0.5rem;
    }
  }

  .item__btn {
    text-align: center;
    cursor: pointer;
    letter-spacing: 0.03em;
    font-size: 0.9rem;
    white-space: nowrap;
    color: #fff;
    padding: 0.33rem 1.66rem;
    background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
    border: 1px solid #301934 ;
    box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
    transition: 0.5s ease;

    &:not(&:disabled):hover {
      box-shadow: inset 0px 0px 7.5rem #301934 ;
      filter: drop-shadow(0px 0px 15px rgba(71, 44, 132, 0.5));
    }

    &:disabled {
      background: linear-gradient(180deg, rgba(217, 217, 217, 0.25) 0%, rgba(100, 100, 100, 0.25) 100%);
      border: 1px solid #a9a9a9;
      box-shadow: inset 0px 0px 15px rgba(127, 127, 127, 0.8);
      color: #d9d9d9;
    }
  }
}
</style>
