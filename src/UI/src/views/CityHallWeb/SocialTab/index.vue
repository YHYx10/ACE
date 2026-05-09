<template>
  <div class="body-tab">
    <div class="body-tab__title">{{ loc("cityHallWeb_117") }}</div>
    <div class="body-tab__desc">This section indicates all your licenses and marital status</div>
    <!-- <div>
      <menu-nav
        :list="navList"
        :currentPage="currentPage"
        :setCurrentPage="setCurrentPage"
      />
    </div> -->
    <div>
      <!-- <component :is="currentPage" /> -->
      <StatusPage />
      <NameEditPage />
    </div>
  </div>
</template>

<script>
/* import MenuNav from "../components/MenuNav"; */
import NameEditPage from "./NameEditPage";
/* import LicensesPage from "./LicensesPage"; */
import StatusPage from "./StatusPage";
import { mapGetters } from "vuex";
export default {
  name: "SocialTab",

  components: {
    /* MenuNav, */
    NameEditPage,
    /* LicensesPage, */
    StatusPage,
  },

  data: function () {
    return {
      currentPage: null,
      navList: [
        {
          key: "NameEditPage",
          text: "cityHallWeb_118",
        },
        {
          key: "LicensesPage",
          text: "cityHallWeb_119",
        },
        {
          key: "StatusPage",
          text: "cityHallWeb_121",
        },
      ],
    };
  },

  computed: {
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    setCurrentPage: function (value) {
      this.currentPage = value;
    },
  },

  mounted() {
    this.currentPage = this.navList[0].key;
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.body-tab {
  &__title {
    margin-bottom: conv(7);
  }

  &__desc {
    margin-bottom: conv(45);
  }

  & > div:last-child {
    display: grid;
    grid-template-columns: 1fr 1fr;
    column-gap: conv(20);
    width: 100%;
    height: conv(607);

    & > div {
      width: 100%;
      height: 100%;
      background: rgba(255, 255, 255, 0.03);
      padding: conv(33) 0 0 conv(38);
    }
  }
}
</style>
