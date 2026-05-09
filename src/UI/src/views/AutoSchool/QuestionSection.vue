<template>
  <div class="autoschool-question">
    <button
      class="close"
      @click="setCurrentSection({ section: 'GeneralSection', data: null })"
    >
      <img src="/img/autoSchool/question/x.svg" alt="" />
    </button>
    <div class="autoschool-title">
      <span>CENTER</span>
 Licensing
    </div>
    <div class="autoschool-descr"> boden Transport</div>

    <div class="autoschool-question_wrap">
      <div class="autoschool-question_header">
        <div>Theoretically test</div>
        <div>
          <div>ASK</div>
          <div>
            {{ this.currentQuestionIndex + 1 }}&nbsp;/&nbsp;{{
              questions.length
            }}
          </div>
        </div>
      </div>

      <div class="autoschool-question_text">
        {{ loc(question.quest) }}
      </div>

      <div class="autoschool-question_select">
        <Answer
          v-for="answer in question.answers"
          :key="answer.id"
          :answer="answer"
          :questionKey="question.key"
          @reset="() => (accept = false)"
        />
      </div>

      <div class="autoschool-question_btn">
        <button
          class="bank-btn confirm"
          @click="confirm"
          v-if="this.currentAnswer !== null && !accept"
        >Confirm
        </button>
        <button class="next" @click="pushAnswer" v-if="accept">
          Continue
        </button>
      </div>

      <div class="autoschool-question_img" v-if="question.image !== ''">
        <div>Question with the picture</div>
        <img :src="`/img/autoSchool/${question.image}.png`" alt="" />
      </div>
    </div>

    <!-- <div class="btn-next" v-if="this.currentAnswer === null">
      {{ loc("AutoSchool_10") }}
    </div>
    <div class="btn-next allowed" v-else @click="pushAnswer">
      {{ loc("AutoSchool_10") }}
    </div> -->
  </div>
  <!-- <div class="question-section">
    <div class="question-section__header">
      <div class="title">{{loc('AutoSchool_8')}}</div>
    </div>
    <div class="question-section__info">
      <div class="current-question">{{loc('AutoSchool_9')}} <span class="current-value">{{this.currentQuestionIndex + 1}}</span> / <span>{{questions.length}}</span></div>
      <div 
        v-if="question.image !== ''"
        class="question-img" 
        v-bind:style="{backgroundImage: 'url(/img/autoSchool/' + question.image + '.png)'}"
      ></div>
      <div class="question-text">{{loc(question.quest)}}</div>
      <div class="variants-answer">
        <Answer
          v-for="answer in question.answers"
          :key="answer.id"
          :answer="answer"
          :questionKey="question.key"
        />
      </div>
      <div class="btn-next" v-if="this.currentAnswer === null">{{loc('AutoSchool_10')}}</div>
      <div 
        class="btn-next allowed" 
        v-else
        @click="pushAnswer"
      >{{loc('AutoSchool_10')}}</div>
    </div>
  </div> -->
</template>

<script>
import { mapGetters, mapMutations, mapState } from "vuex";

import Answer from "./Answer";

export default {
  name: "GeneralSection",

  data: function () {
    return {
      answersOnQuestions: [],
      accept: false,
    };
  },

  components: {
    Answer,
  },

  computed: {
    ...mapState("autoSchool", [
      "question",
      "questions",
      "currentAnswer",
      "currentQuestionKey",
      "currentQuestionIndex",
    ]),
    ...mapGetters("localization", ["loc"]),
    question: function () {
      return this.questions[this.currentQuestionIndex];
    },
  },

  methods: {
    ...mapMutations("autoSchool", [
      "switchNextQuestion",
      "setCurrentSection",
      "switchDropQuestion",
    ]),
    pushAnswer: function () {
      let keyQuest = this.currentQuestionKey;
      let keyAnswer = this.currentAnswer;
      let newObj = { keyQuest, keyAnswer };
      this.answersOnQuestions.push(newObj);
      this.accept = false;
      if (this.currentQuestionIndex === this.questions.length - 1) {
        window.mp.trigger(
          "school:sendAnswer",
          JSON.stringify(this.answersOnQuestions)
        );
        this.setCurrentSection({ section: "ResultSection", data: "theory" });
        this.switchDropQuestion();
      } else {
        this.switchNextQuestion();
      }
    },
    confirm() {
      if (this.currentAnswer !== null) {
        this.accept = true;
      }
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.autoschool-question {
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: conv(58);
  width: conv(1920);

  &_wrap {
    width: conv(736);
    display: flex;
    flex-direction: column;
    position: relative;
  }

  .close {
    position: absolute;
    top: conv(40);
    right: conv(40);
    width: conv(50);
    height: conv(50);
    cursor: pointer;
    display: flex;
    justify-content: center;
    align-items: center;
    border: none;
    background: none;
    outline: none;

    img {
      width: conv(30);
      height: conv(30);
    }
  }

  .autoschool-title {
    margin-bottom: conv(10);
  }

  .autoschool-descr {
    margin-bottom: conv(118);
  }

  &_header {
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    white-space: nowrap;

    & > div {
      font-weight: 700;
      font-size: conv(32);
      line-height: conv(38);

      &:first-child {
        color: #ffffff;
      }

      &:last-child {
        display: flex;
        align-items: center;

        div {
          &:first-child {
            color: #ffffff;
            margin-right: conv(29);
          }

          &:last-child {
            text-transform: uppercase;
            color: #ffcc47;
          }
        }
      }
    }
  }

  &_text {
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    background: rgba(255, 255, 255, 0.05);
    margin: conv(25) 0 conv(26);
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    color: #ffffff;
    padding: conv(40) conv(20) conv(40);
  }

  &_select {
    width: 100%;
    display: flex;
    flex-direction: column;
    margin-bottom: conv(30);
  }

  &_btn {
    button {
      width: conv(310);
      height: conv(75);
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      text-align: center;
      text-transform: uppercase;
      cursor: pointer;
      color: #ffffff;
    }

    .confirm {
      background: linear-gradient(
        180deg,
        rgba(71, 44, 132, 0.25) 0%,
        rgba(75, 0, 130, 0.25) 100%
      );
      outline: 1px solid #301934 ;
      box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
      &:hover {
        box-shadow: inset 0px 0px 3.789rem rgba(75, 0, 130, 0.86);
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.35) 0%,
          rgba(75, 0, 130, 0.35) 100%
        );
      }
    }

    .next {
      background: rgba(255, 255, 255, 0.15);
      transition: 0.3s ease;

      &:hover {
        background: rgba(255, 255, 255, 0.3);
      }
    }
  }

  &_img {
    position: absolute;
    top: conv(8);
    transform: translateX(-100%);
    left: conv(-30);
    width: conv(427);

    div {
      margin-bottom: conv(26);
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      color: #ffffff;
    }

    img{
      width: 100%;
      height: conv(272);
    }
  }
}
/* .question-section {
  display: flex;
  flex-flow: column;
  &__header {
    background: rgba(134, 0, 137, 0.13);
    backdrop-filter: blur(0.9rem);
    display: flex;
    flex-flow: column;
    align-items: center;
    justify-content: center;
    width: 100%;
    .title {
      color: #ffffff;
      width: 100%;
      font-weight: bold;
      text-align: center;
      padding: 1.75rem 0;
      font-size: 1.65rem;
      line-height: 2rem;
    }
  }
  &__info {
    width: 100%;
    display: flex;
    flex-flow: column;
    align-items: flex-start;
    padding: 3.2rem 17rem 0 17rem;
    .current-question {
      display: flex;
      align-items: center;
      justify-content: center;
      padding: 0.75rem 1.2rem;
      border: 1px solid #ad00ff;
      box-sizing: border-box;
      border-radius: 2.95rem;
      font-weight: 500;
      font-size: 0.75rem;
      line-height: 0.4rem;
      margin-bottom: 1.5rem;
      white-space: pre;
      color: #ffffff;
      .current-value {
        color: #ad00ff;
      }
    }
    .question-img {
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      height: 9.85rem;
      width: 25.8rem;
    }
    .question-text {
      font-style: normal;
      font-weight: bold;
      font-size: 1.25rem;
      line-height: 1.45rem;
      color: #ffffff;
      margin-top: 2.4rem;
      margin-bottom: 3.2rem;
    }
    .variants-answer {
      display: grid;
      width: 44.35rem;
      grid-template-columns: repeat(2, 1fr);
      grid-auto-rows: auto;
      grid-row-gap: 0.95rem;
      grid-column-gap: 3.95rem;
    }
    .btn-next {
      display: flex;
      align-items: center;
      justify-content: center;
      box-sizing: border-box;
      font-style: normal;
      font-weight: 500;
      color: #ffffff;
      transition: 0.2s;
      margin-top: 3.8rem;
      padding: 0 1.5rem;
      font-size: 0.95rem;
      height: 3.65rem;
      min-height: 3.65rem;
      border: 0.1rem solid transparent;
      background: rgba(63, 37, 89, 0.26);
      border-radius: 2.55rem;
      &.allowed {
        background: transparent;
        border: 0.1rem solid #9b40f7;
        &:hover {
          background: #9b40f7;
          box-shadow: 0 0.5rem 1.5rem -0.5rem rgba(173, 0, 255, 0.8);
          transition: 0.2s;
        }
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
  }
} */
</style>