<template>
  <div class="city-hall-web">
    <template
      ><!-- v-if="isRegistered" -->
      <div class="body">
        <div class="nav-list">
          <nav-item
            v-for="item in navList"
            :key="item.key"
            :item="item"
            :currentTab="currentTab"
            :setCurrentTab="setCurrentTab"
          />
        </div>
        <component :is="currentTab" />
        <simple-modal v-show="modalData.show" />
      </div>
    </template>
    <!-- <Registration v-else /> -->
    <button class="close" @click="close">
      <img src="/img/autoSchool/question/x.svg" alt="" />
    </button>
  </div>
</template>

<script>
import Registration from "./Registration";
import NavItem from "./components/NavItem";
import LegalAidTab from "./LegalAidTab";
import EmploymentTab from "./EmploymentTab";
import VoteTab from "./VoteTab";
import NewsTab from "./NewsTab";
import SocialTab from "./SocialTab";
import RelationshipsTab from "./RelationshipsTab";
import HealthCareTab from "./HealthCareTab";
import VehicleTaxTab from "./VehicleTaxTab";
import CultureTab from "./CultureTab";
import CommunityTab from "./CommunityTab";
import DonationsTab from "./DonationsTab";
import SimpleModal from "./components/SimpleModal";

import { mapState } from "vuex";
export default {
  name: "CityHallWeb",

  components: {
    Registration,
    NavItem,
    LegalAidTab,
    EmploymentTab,
    VoteTab,
    NewsTab,
    SocialTab,
    RelationshipsTab,
    HealthCareTab,
    VehicleTaxTab,
    CultureTab,
    CommunityTab,
    DonationsTab,
    SimpleModal,
  },
  data: function () {
    return {
      currentTab: "",
      navList: [
        /* {
          icon: "law",
          text: "chall:cat:1",
          key: "LegalAidTab",
        }, */
        {
          icon: "portfolio",
          text: "chall:cat:2",
          key: "EmploymentTab",
        },
        /* {
          icon: "vote",
          text: "chall:cat:3",
          key: "VoteTab",
        },
        {
          icon: "world-grid",
          text: "chall:cat:4",
          key: "NewsTab",
        }, */
        {
          icon: "verified",
          text: "chall:cat:5",
          key: "SocialTab",
        },
        /* {
          icon: "engagement-rings",
          text: "chall:cat:6",
          key: "RelationshipsTab",
        }, */
        /* {
          icon: "pharmacy",
          text: "chall:cat:7",
          key: "HealthCareTab",
        },
        {
          icon: "sedan-car-front",
          text: "chall:cat:8",
          key: "VehicleTaxTab",
        },
        {
          icon: "theater",
          text: "chall:cat:9",
          key: "CultureTab",
        }, */
        {
          icon: "users",
          text: "chall:cat:10",
          key: "CommunityTab",
        },
        {
          icon: "savings",
          text: "chall:cat:11",
          key: "DonationsTab",
        },
      ],
    };
  },

  methods: {
    setCurrentTab: function (key) {
      this.currentTab = key;
    },
    close() {
      window.mp.trigger("cityHallWeb:close");
    },
  },

  computed: {
    ...mapState("cityHallWeb", ["isRegistered", "modalData"]),
  },

  mounted() {
    this.currentTab = this.navList[0].key;
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.city-hall-web {
  display: flex;
  flex-flow: column;
  align-items: center;
  width: 100%;
  height: 100vh;
  position: absolute;
  left: 0;
  top: 0;
  z-index: 1;
  background-color: #f3f5f7;
  background: linear-gradient(
    72.44deg,
    rgba(19, 20, 21, 0.99) 0%,
    rgba(31, 34, 37, 0.99) 100%
  );
  padding-top: conv(65);

  &::after {
    z-index: 2;
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: conv(611);
    background: linear-gradient(
      180deg,
      rgba(0, 0, 0, 0.3) 0%,
      rgba(0, 0, 0, 0) 100%
    );
  }

  /* .title-wrap {
    width: 100%;
    display: flex;
    flex-flow: column;
    align-items: center;
    font-family: Merriweather;
    margin: 0.9rem 0;
    &__value {
      font-size: 2.05rem;
      line-height: 2.6rem;
      font-weight: bold;
      color: #293340;
    }
    &__decorate {
      display: flex;
      align-items: center;
      .star {
        width: 0.9rem;
        height: 0.9rem;
        margin-right: 0.95rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        background-image: url("/img/cityHallWeb/star-decore.svg");
        &:first-child,
        &:last-child {
          position: relative;
          &:before,
          &:after {
            content: "";
            position: absolute;
            background: #758795;
            height: 1px;
          }
          &:before {
            top: 0.4rem;
            width: 4.7rem;
          }
          &:after {
            top: 0.6rem;
            width: 2.6rem;
          }
        }
        &:first-child {
          &:before,
          &:after {
            left: -0.3rem;
            transform: translateX(-100%);
          }
        }
        &:last-child {
          margin-right: 0;
          &:before,
          &:after {
            right: -0.3rem;
            transform: translateX(100%);
          }
        }
      }
    }
  } */

  .body {
    display: flex;
    flex-direction: column;
    z-index: 3;
    width: conv(1635);
    max-width: conv(1635);
    height: conv(920);
    max-height: conv(920);

    .nav-list {
      width: 100%;
      display: flex;
      height: conv(145);
      min-height: conv(145);
      margin-bottom: conv(36);
      justify-content: space-between;
    }
  }
}
</style>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.city-hall-web {
  * {
    font-family: "Akrobat" !important;
  }

  .btn {
    display: flex;
    align-items: center;
    justify-content: center;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
    position: relative;
    transition: 0.3s;
    &:hover {
      transition: 0.3s;
    }
  }
  .body-tab {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    position: relative;

    &__title {
      font-weight: 800;
      font-size: conv(48);
      line-height: conv(58);
      text-transform: uppercase;
      color: #ffffff;
      white-space: nowrap;
    }

    &__desc {
      font-weight: 600;
      font-size: conv(18);
      line-height: conv(22);
      text-transform: uppercase;
      color: #ffffff;
    }

    & > div {
      display: flex;
      flex-flow: column;
    }
    .page {
      display: flex;
      flex-flow: column;
      width: 100%;

      &__title {
        font-weight: 800;
        font-size: conv(36);
        line-height: conv(43);
        text-transform: uppercase;
        color: #ffffff;
        white-space: nowrap;
      }

      &__prompt {
        font-family: Roboto;
        font-style: normal;
        font-weight: normal;
        font-size: 0.8rem;
        line-height: 0.95rem;
        color: rgba(70, 85, 95, 0.6);
        margin-bottom: 1.2rem;
      }
      &__desc {
        display: flex;
        align-items: center;
        position: relative;
        font-family: Roboto;
        font-style: normal;
        font-weight: bold;
        font-size: 0.9rem;
        line-height: 1.05rem;
        text-transform: uppercase;
        color: #46555f;
        margin-bottom: 0.7rem;
        padding-left: 1rem;
        &:before {
          content: "";
          width: 0.4rem;
          height: 0.4rem;
          border-radius: 50%;
          position: absolute;
          left: 0;
          background: #e00b29;
        }
      }
      &__btn {
        background: #e00b29;
        border-radius: 0.3rem;
        text-transform: uppercase;
        font-style: normal;
        font-weight: bold;
        font-size: 1.1rem;
        color: #ffffff;
        height: 3.5rem;
        min-height: 3.5rem;
        &:hover {
          background: #c00621;
        }
      }
      input {
        box-sizing: border-box;
        background-color: transparent;
        outline: none;

        &::-webkit-outer-spin-button,
        &::-webkit-inner-spin-button {
          -webkit-appearance: none;
        }
      }
      input {
        padding: 0 1rem;
        height: 3.4rem;
        min-height: 3.4rem;
      }
      .menu-drop {
        input {
          border: none;
          margin: 0;
        }
      }
    }
  }

  .close {
    position: absolute;
    top: conv(40);
    right: conv(40);
    width: conv(50);
    height: conv(50);
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    background: none;
    border: none;
    outline: none;
    z-index: 10;

    img {
      height: conv(30);
    }
  }
}
</style>
