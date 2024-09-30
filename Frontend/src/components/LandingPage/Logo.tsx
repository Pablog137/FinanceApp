export default function Logo({ textColor }: { textColor: string }) {
  return (
    <section className="col-span-6 lg:col-span-3th me-5">
      <div className="col-span-12 flex items-center gap-5">
        <img
          src="/images/logo.svg"
          alt="image"
          style={{ width: "40px", height: "40px" }}
        />
        <h1 className={`${textColor} text-3xl  font-bold`}>FinanceApp</h1>
      </div>
    </section>
  );
}
