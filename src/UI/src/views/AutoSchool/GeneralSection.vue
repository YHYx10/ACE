<template>
  <component
    :is="CurrentPage"
    @select-exam="(idx, keys) => selectExam(idx, keys)"
    :typeExam="typeExam"
  />
  <!-- <div class="general-section">
    <div class="general-section__header">
      <div class="title">{{loc(this.currentSchoolWelcome)}}</div>
      <div class="subtitle">{{loc('AutoSchool_3')}}</div>
    </div>
    <div class="general-section__choice">
      <div class="block-interaction theory">
        <div class="block-interaction__desc">{{loc('AutoSchool_4')}}</div>
        <div class="btn btn-apply" @click="startExam('theory')">{{loc('AutoSchool_5')}}</div>
        <div 
          class="btn btn-result allowed"
          v-if="Object.entries(this.answersOnQuestions).length !== 0"
          @click="showResult('theory')"
        >{{loc('AutoSchool_6')}}</div>
        <div 
          class="btn btn-result"
          v-else
        >{{loc('AutoSchool_6')}}</div>
      </div>
      <div class="block-interaction practic">
        <div class="block-interaction__desc">{{loc('AutoSchool_7')}}</div>
        <div class="btn btn-apply" @click="startExam('practic')">{{loc('AutoSchool_5')}}</div>
        <div 
          class="btn btn-result allowed"
          v-if="Object.entries(this.practicResults).length !== 0"
          @click="showResult('practic')"
        >{{loc('AutoSchool_6')}}</div>
        <div 
          class="btn btn-result"
          v-else
        >{{loc('AutoSchool_6')}}</div>
      </div>
    </div>
  </div> -->
</template>

<script>
import { mapState, mapMutations, mapGetters } from "vuex";
import General from "./General.vue";
import Main from "./Main.vue";

export default {
  name: "GeneralSection",
  components: { General, Main },
  data() {
    return {
      CurrentPage: "Main",
      typeExam: 0,
    };
  },

  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("autoSchool", [
      "answersOnQuestions",
      "practicResults",
      "currentSchoolWelcome",
    ]),
  },

  methods: {
    ...mapMutations("autoSchool", ["setCurrentSection", "setCurrentExamType"]),

    startExam: function (typeExam) {
      window.mp.trigger("school:startExam", typeExam);
    },

    showResult: function (typeResult) {
      this.setCurrentSection({ section: "ResultSection", data: typeResult });
    },

    selectExam(idx, keys) {
      window.mp.trigger("school:selectExam", keys);
      this.typeExam = idx;
      this.setCurrentExamType(idx)
      // if(idx >= 0 && idx < 3){
      //   this.typeExam  = 0;
      // }else if(idx == 4){
      //   this.typeExam = 1;
      // }else{
      //   this.typeExam = 2;
      // }
      // this.typeExam = idx > 4 ? 1 : 0;
      this.CurrentPage = "General";
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}
/* .general-section {
  width: 100%;
  &__header {
    background: rgba(134, 0, 137, 0.13);
    backdrop-filter: blur(0.9rem);
    display: flex;
    flex-flow: column;
    align-items: center;
    justify-content: center;
    .title,
    .subtitle {
      color: #ffffff;
      width: 100%;
      font-weight: bold;
      text-align: center;
    }
    .title {
      padding: 2.6rem 0 1.6rem 0;
      font-size: 2.05rem;
      line-height: 2.5rem;
      text-transform: uppercase;
      border-bottom: 1px solid rgba(255, 255, 255, 0.19);
    }
    .subtitle {
      padding: 0.95rem 0;
      font-size: 1.65rem;
      line-height: 2rem;
    }
  }
  &__choice {
    display: flex;
    align-items: center;
    justify-content: center;
    margin-top: 3.7rem;
    .block-interaction {
      display: flex;
      flex-flow: column;
      align-items: flex-start;
      justify-content: flex-end;
      border-radius: 0.85rem;
      padding-bottom: 1.4rem;
      width: 23.55rem;
      height: 23.9rem;
      min-height: 23.9rem;
      overflow: hidden;
      background-size: cover;
      background-repeat: no-repeat;
      background-position: center;
      margin-right: 3.7rem;
      &:last-child {
        margin-right: 0;
      }
      &.theory {
        background-image: url("/img/autoSchool/bg-theory.png");
      }
      &.practic {
        background-image: url("/img/autoSchool/bg-practic.png");
      }
      &__desc {
        padding: 0 2.65rem 1.55rem 2.65rem;
        width: 100%;
        border-bottom: 1px solid rgba(169, 158, 158, 0.26);
        font-weight: bold;
        font-size: 1.65rem;
        line-height: 1.95rem;
        text-transform: uppercase;
        color: #ffffff;
      }
      .btn {
        display: flex;
        align-items: center;
        justify-content: center;
        margin-left: 2.65rem;
        box-sizing: border-box;
        font-style: normal;
        font-weight: 500;
        color: #ffffff;
        transition: 0.2s;
        &:hover {
          transition: 0.2s;
        }
        &.btn-apply {
          margin-top: 1.25rem;
          margin-bottom: 0.7rem;
          padding: 0 1.5rem;
          font-size: 0.95rem;
          height: 3.65rem;
          min-height: 3.65rem;
          border: 0.1rem solid #9b40f7;
          border-radius: 2.55rem;
          &:hover {
            background: #9b40f7;
          }
          &:after {
            content: "";
            width: 1.3rem;
            height: 1.3rem;
            background-image: url("/img/autoSchool/btn-arr.svg");
            background-size: contain;
            background-position: center;
            background-repeat: no-repeat;
            margin-left: 1.9rem;
          }
        }
        &.btn-result {
          font-size: 0.75rem;
          height: 2.95rem;
          min-height: 2.95rem;
          padding: 0 1.2rem;
          background: #26192e;
          border-radius: 2.05rem;
          &.allowed {
            &:hover {
              background: #ffffff;
              color: #000000;
              transition: 0.2s;
            }
          }
        }
      }
    }
  }
} */
</style>
