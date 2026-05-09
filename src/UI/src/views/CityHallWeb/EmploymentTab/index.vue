<template>
  <div class="body-tab employment-tab">
    <div class="body-tab__title">{{ loc("cityHallWeb_25") }}</div>
    <div class="body-tab__desc">{{ loc("Here you can familiarize yourself with the works of our state.All vacancies are presented below") }}</div>
    <div>
      <div>
        <menu-nav
          :list="jobs"
          :currentPage="currentJob"
          :setCurrentPage="setCurrentJob"
        />
      </div>
      <div>
        <job-wrap :selectedJob="selectedJob" />
      </div>
    </div>
  </div>
</template>

<script>
//import { mapState } from 'vuex'
import MenuNav from "../components/MenuNav";
import JobWrap from "./JobWrap";
import worksList from "../../OptionsMenu/Tabs/InformationTab/sections/WorksSection/worksList";
import { mapGetters } from "vuex";
export default {
  name: "EmploymentTab",

  components: {
    MenuNav,
    JobWrap,
  },

  data: function () {
    return {
      currentJob: null,
      workList: worksList,
      jobs: [],
    };
  },

  computed: {
    ...mapGetters("localization", ["loc"]),

    selectedJob: function () {
      return this.jobs.find((element) => element.key === this.currentJob);
    },
  },

  methods: {
    setCurrentJob: function (value) {
      this.currentJob = value;
    },
  },

  created() {
    this.jobs = this.workList.filter((item) => !item.locked && item.point != 5);
    this.jobs.forEach((job) => {
      job.key = job.title;
      job.text = job.title;
    });
    this.currentJob = this.jobs[0].key;
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
    margin-bottom: conv(38);
  }

  & > div:last-child{
    display: flex;
    flex-direction: row;
    width: 100%;
    height: 100%;

    & > div:last-child{
      width: 100%;
      height: conv(614);
    }
  }
}
</style>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.city-hall-web {
  .body-tab {
    &.employment-tab {
      .menu-nav {
        padding-right: 1.1rem;
        margin-left: -1.1rem;
        padding-left: 1rem;
        transform: rotateY(180deg);
        max-height: conv(614);
        height: conv(614);
        overflow-y: auto;

        &::-webkit-scrollbar {
          background: rgba(255, 255, 255, 0.05);
          width: conv(5);

          &-thumb {
            background: #301934 ;
          }
        }
        & > div {
          transform: rotateY(-180deg);
        }
      }
    }
  }
}
</style>
